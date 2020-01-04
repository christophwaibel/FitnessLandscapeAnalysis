using System;
using FLA = FitnessLandscapeAnalysis;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CEC20
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Enter path that contains input and output files, ending with \\");
            string path_all = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Enter input file name, e.g. \"X.csv\"");
            string filename_X = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Enter filename of cost values y, e.g. \"y.csv\"");
            string filename_y = Console.ReadLine();

            //Console.WriteLine();
            //Console.WriteLine("enter filename of csv that contains domain information (lower and upper bounds)");
            //string filename_domain = Console.ReadLine();    //functions.csv
            string filename_domain = "functions.csv";

            // load X
            string[] text = FLA.Misc.IO.ReadTextFile(path_all + filename_X);
            double[][] X = new double[text.Length][];
            for (int i = 0; i < text.Length; i++)
            {
                X[i] = Array.ConvertAll(text[i].Split(new char[] { ';', ',' }), new Converter<string, double>(Double.Parse));
            }
            int n = X[0].Length;
            int P = X.Length;

            // load y that contains all problems
            text = FLA.Misc.IO.ReadTextFile(path_all + filename_y);
            string[] problem_id = null;
            problem_id = text[0].Split(new char[] { ';', ',' });
            double[][] y = new double[text.Length - 1][];
            for (int i = 1; i < text.Length; i++)
            {
                y[i - 1] = new double[problem_id.Length];
                double[] y_oneline = Array.ConvertAll(text[i].Split(new char[] { ';', ',' }), new Converter<string, double>(Double.Parse));
                Parallel.For(0, problem_id.Length - 1, j =>
                {
                    y_oneline.CopyTo(y[i - 1], 0);
                });
            }

            // load/enter xub, xlb... could also be a textfile. read from a file and use problem_index as identifier
            //contains 
            //          string [] problem_id_domain (filenames)
            // and      string [] domain
            // split with ';', not ','!
            //make a dictionary out of it
            //if problem_id matches with problem_id_domain, take that domain and convert it into lower and upper bounds (splitting with ',' and deleting [ ])
            // special cases: nVar^2, pi
            //skip all that have NaN in y.csv. dont even bother splitting the domain-string
            text = FLA.Misc.IO.ReadTextFile(path_all + filename_domain);
            Dictionary<string, string> dict = new Dictionary<string, string>();     // key is problem id, value is domain
            for (int i = 1; i < text.Length; i++)
            {
                string[] one_line = text[i].Split(';');
                dict.Add(one_line[0], one_line[2]);
            }

            bool[] validsample = new bool[problem_id.Length];
            double[][] y_transp = FLA.Misc.Transformation.Transpose(y);
            for (int i = 0; i < y_transp.Length; i++)
                if (!Double.IsNaN(y_transp[i][0]) && (FLA.Misc.Statistics.Min(y_transp[i]) != 0 && FLA.Misc.Statistics.Max(y_transp[i]) != 0))
                    validsample[i] = true;

            //foreach(string id in problem_id) 
            //    Console.WriteLine("problem {0} has domain: {1}", id, dict[id]);
            //Console.ReadKey();

            double?[] fdc = new double?[problem_id.Length];
            string[] chars_to_remove = new string[] { "[", "]" };
            for (int i = 0; i < problem_id.Length; i++)
            {
                if (!validsample[i])
                {
                    fdc[i] = null;
                }
                else
                {
                    double[] xub = new double[n];
                    double[] xlb = new double[n];
                    string[] domain_split = dict[problem_id[i]].Split(',');
                    for (int j = 0; j < domain_split.Length; j++)
                    {
                        foreach (string c in chars_to_remove)
                        {
                            domain_split[j] = domain_split[j].Replace(c, string.Empty);
                        }
                    }

                    try
                    {
                        double xlb_1 = Convert.ToDouble(domain_split[0]);
                        double xub_1 = Convert.ToDouble(domain_split[1]);
                        for (int j = 0; j < n; j++)
                        {
                            xub[j] = xub_1;
                            xlb[j] = xlb_1;
                        }
                    }
                    catch (Exception e)
                    {
                        string prob = problem_id[i];
                        switch (prob)
                        {
                            case "f036":    // [-nVar^2,nVar^2]
                                for (int j = 0; j < n; j++)
                                {
                                    xlb[j] = Math.Pow(n, 2) * -1;
                                    xub[j] = Math.Pow(n, 2);
                                }
                                break;
                            case "f049":
                            case "f188":    // [-pi,pi]
                                for (int j = 0; j < n; j++)
                                {
                                    xlb[j] = Math.PI * -1;
                                    xub[j] = Math.PI;
                                }
                                break;
                            case "f078":
                            case "f107":
                            case "f108":    // [-15,-5;-3,3]
                                xlb[0] = -15;
                                xlb[1] = -3;
                                xub[0] = -5;
                                xub[1] = 3;
                                break;
                            case "f099":
                            case "f100":     // [-2pi, 2pi]
                                for (int j = 0; j < n; j++)
                                {
                                    xlb[j] = Math.PI * -2;
                                    xub[j] = Math.PI * 2;
                                }
                                break;
                            case "f178":
                                for (int j = 0; j < n; j++)
                                {
                                    xlb[j] = 0;
                                    xub[j] = Math.PI;
                                }
                                break;
                            case "f216":
                                for (int j = 0; j < n; j++)
                                {
                                    xlb[j] = Math.PI * -5;
                                    xub[j] = Math.PI * 5;
                                }
                                break;
                        }
                    }
                    fdc[i] = FLA.Metrics.FDC(X, y_transp[i], xlb, xub);
                    if (Double.IsNaN((double)fdc[i]))
                        Console.WriteLine("{0} is NaN", problem_id[i]);
                }
            }

            // write fdc values and problem_id into a csv. each problem per row
            string[] write_fdc = new string[problem_id.Length];
            for (int i=0; i<problem_id.Length; i++)
            {
                write_fdc[i] = Convert.ToString(fdc[i]) + "," + problem_id[i];
            }
            string fdc_path = "FDC_" + filename_y;
            FLA.Misc.IO.WriteTextFile(path_all, fdc_path, write_fdc);

            Console.WriteLine();
            Console.WriteLine("FDC file written to {0}", fdc_path);
            Console.WriteLine("Hit any key to exit");
            Console.ReadKey();
        }
    }
}
