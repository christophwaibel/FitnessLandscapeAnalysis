using System;
using FLA = FitnessLandscapeAnalysis;
using System.IO;

namespace CEC20
{
    class Program
    {
        static void Main(string[] args)
        {
            // load X
            Console.WriteLine();
            Console.WriteLine("Enter path and filename of Input sequence X, e.g. \"c:\\temp\\x.csv\". Should be a csv or txt file, parameters comma separated per row");
            string path_X = Console.ReadLine();
            string[] text = FLA.Misc.IO.ReadTextFile(path_X);
            double[][] X = new double[text.Length][];
            for (int i = 0; i < text.Length; i++)
            {
                X[i] = Array.ConvertAll(text[i].Split(new char[] { ';', ',' }), new Converter<string, double>(Double.Parse));
            }
            int n = X[0].Length;
            int P = X.Length;

            // load y
            Console.WriteLine();
            Console.WriteLine("Enter path and filename of cost values y, e.g. \"c:\\temp\\y.csv\"");
            string path_y = Console.ReadLine();
            text = FLA.Misc.IO.ReadTextFile(path_y);
            int problem_index = -1;
            string problem_id = null;
            if (text[0].Split(',').Length > 1)
            {
                Console.WriteLine("This output file contains data for more than one problem. Specify index of problem (>= 0) for FDC calculation:");
                problem_index = Convert.ToInt32(Console.ReadLine());
                problem_id = text[0].Split(new char[] { ';', ',' })[problem_index];
            }
            else
            {
                problem_id = text[0];
            }
            double[] y = new double[text.Length - 1];

            if (problem_index >= 0)
                for (int i = 1; i < text.Length; i++)
                    y[i - 1] = Array.ConvertAll(text[i].Split(new char[] { ';', ',' }), new Converter<string, double>(Double.Parse))[problem_index];
            else
                for (int i = 1; i < text.Length; i++)
                    y[i - 1] = Convert.ToDouble(text[i]);

            // load/enter xub, xlb... could also be a textfile. read from a file and use problem_index as identifier
            Console.WriteLine();
            Console.WriteLine("Enter lower bound. For now, all the same for all i in n");
            string read_lb = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter upper bound. For now, all the same for all i in n");
            string read_ub = Console.ReadLine();

            // convert lb and ub to doubles
            double xlb_i = Convert.ToDouble(read_lb);
            double xub_i = Convert.ToDouble(read_ub);
            double[] xub = new double[n];
            double[] xlb = new double[n];
            for (int i = 0; i < n; i++)
            {
                xub[i] = xub_i;
                xlb[i] = xlb_i;
            }

            // rescale X to domain, because it is only 0 to 1
            for (int j = 0; j < P; j++)
                for (int i = 0; i < n; i++)
                    X[j][i] = X[j][i] * (xub[i] - xlb[i]) + xlb[i];

            //double fdc = Metrics.FDC(X, y, xlb, xub, 0.0, new double[10] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
            double fdc = FLA.Metrics.FDC(X, y, xlb, xub);
            Console.WriteLine();
            Console.WriteLine("FDC of problem {0}: {1}", problem_id, fdc);
        }
    }
}
