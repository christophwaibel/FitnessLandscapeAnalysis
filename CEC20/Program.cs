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
                    double xlb_1;
                    double xub_1;
                    try
                    {
                        xlb_1 = Convert.ToDouble(domain_split[0]);
                        xub_1 = Convert.ToDouble(domain_split[1]);
                        for (int j = 0; j < n; j++)
                        {
                            xub[j] = xub_1;
                            xlb[j] = xlb_1;
                        }
                        fdc[i] = FLA.Metrics.FDC(X, y_transp[i], xlb, xub);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0}: {1}", problem_id[i], e);
                        fdc[i] = null;
                    }
                }
            }
            Console.ReadKey();


            //string read_lb = Console.ReadLine();
            //Console.WriteLine();
            //Console.WriteLine("Enter upper bound. For now, all the same for all i in n");
            //string read_ub = Console.ReadLine();

            //// convert lb and ub to doubles
            //double xlb_i = Convert.ToDouble(read_lb);
            //double xub_i = Convert.ToDouble(read_ub);
            //double[] xub = new double[n];
            //double[] xlb = new double[n];
            //for (int i = 0; i < n; i++)
            //{
            //    xub[i] = xub_i;
            //    xlb[i] = xlb_i;
            //}

            //// rescale X to domain, because it is only 0 to 1
            //for (int j = 0; j < P; j++)
            //    for (int i = 0; i < n; i++)
            //        X[j][i] = X[j][i] * (xub[i] - xlb[i]) + xlb[i];

            ////double fdc = Metrics.FDC(X, y, xlb, xub, 0.0, new double[10] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
            //double fdc = FLA.Metrics.FDC(X, y, xlb, xub);
            //Console.WriteLine();
            //Console.WriteLine("FDC of problem {0}: {1}", problem_id, fdc);
        }
    }
}
