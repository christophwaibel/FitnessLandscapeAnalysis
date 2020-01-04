using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MetaheuristicsLibrary.TestFunctions;

// TO DO: in class Sequence.cs, it should contain the bounds and take care of scaling/normalization. currently, Sequence creates values 0-1, and the scaling happens in Program.cs


namespace FitnessLandscapeAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fitness Landscape Analysis Toolbox 0.1");
            Console.WriteLine("christophwaibel | waibel@arch.ethz.ch");
            Console.WriteLine();
            Console.WriteLine("Enter option: \"FDC\"; \"WriteRandomWalks\"; ");
            string command = Console.ReadLine();
            //SampleTestFunctions();

            //RandomWalksSequenceCreation();
            if (command == "FDC")
            {
                ComputeFDC();
            }
            else if (command == "WriteRandomWalks")
            {
                RandomWalksSequenceCreation();
            }

            Console.WriteLine("hit any key to exit");
            Console.ReadKey();
        }


        static void ComputeFDC()
        {
            //
            Console.WriteLine();
            Console.WriteLine("//////////////////////////////////////////////////////");
            Console.WriteLine("///////// Fitness Distance Correlation ///////////////");
            Console.WriteLine("//////////////////////////////////////////////////////");
            Console.WriteLine();
            Console.WriteLine("It needs: ");
            Console.WriteLine("1) Input sequence with parameters in the columns and samples in the rows");
            Console.WriteLine("2) Cost values with samples per row. It might contain values for several problems in the columns. First line MUST be a name string of the problem.");
            Console.WriteLine("3) File with problem domain (lower and upper bounds), min solution parameters and min cost value (if known)");
            Console.WriteLine();

            // load X
            Console.WriteLine();
            Console.WriteLine("Enter path and filename of Input sequence X, e.g. \"c:\\temp\\x.csv\". Should be a csv or txt file, parameters comma separated per row");
            string path_X = Console.ReadLine();
            string[] text = Misc.IO.ReadTextFile(path_X);
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
            text = Misc.IO.ReadTextFile(path_y);
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
            for (int i=0; i<n; i++)
            {
                xub[i] = xub_i;
                xlb[i] = xlb_i;
            }

            // rescale X to domain, because it is only 0 to 1
            for (int j = 0; j < P; j++)
                for (int i = 0; i < n; i++)
                    X[j][i] = X[j][i] * (xub[i] - xlb[i]) + xlb[i];

            //double fdc = Metrics.FDC(X, y, xlb, xub, 0.0, new double[10] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
            double fdc = Metrics.FDC(X, y, xlb, xub);
            Console.WriteLine();
            Console.WriteLine("FDC of problem {0}: {1}", problem_id, fdc);
        }


        /// <summary>
        /// Sample test functions using input sequence
        /// </summary>
        /// <param name="args"></param>
        static void SampleTestFunctions()
        {
            int n = 40;                 // problem dimension. do it for n=10 and n=20. only for constraint, only for n=13
            List<Func<double[], double>> tf = new List<Func<double[], double>>();


            //________________________________________________________________________________________________________
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // for BS19 paper
            //int k = (n + 1) * 10;         // sequence size. Morris is (n+1)*10. sobol is (n+2)*100 or 10000
            //string filepath = @"C:\Users\chwaibel\DATA\PROJEKTE\18_FitnessLandscapeAnalysis\04_CASESTUDY\BS19 CaseStudies\TestFuncs\MorrisAnalysis\Input\Morris_inputs_n13_large.csv";
            //string outputpath = @"C:\Users\chwaibel\DATA\PROJEKTE\18_FitnessLandscapeAnalysis\04_CASESTUDY\BS19 CaseStudies\TestFuncs\MorrisAnalysis\Output\";


            //tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.B_Sphere);     // [-1, 1] for all x
            //tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.L_Ackley);     // [-32.768, 32.768] for all x
            //tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.L_Rastrigin);    // [-5.12, 5.12] for all x
            //tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.V_Rosenbrock); // [-2.048, 2.048] for all x
            //double[] lb = new double[tf.Count];
            //double[] ub = new double[tf.Count];
            //lb[0] = -1;
            //lb[1] = -32.768;
            //lb[2] = -5.12;
            //lb[3] = -2.048;
            //ub[0] = 1;
            //ub[1] = 32.768;
            //ub[2] = 5.12;
            //ub[3] = 2.048;

            //tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.C_MichalewiczSchoenauer); // only for n=13. domain: [0,1] for i=0-8 and 12; [0,100] for i=9-11
            //double[] lb = new double[13];
            //double[] ub = new double[13];
            //for (int i = 0; i < 9; i++)
            //{
            //    lb[i] = 0;
            //    ub[i] = 1;
            //}
            //lb[12] = 0;
            //ub[12] = 1;
            //for (int i = 9; i < 12; i++)
            //{
            //    lb[i] = 0;
            //    ub[i] = 100;
            //}
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //________________________________________________________________________________________________________





            //________________________________________________________________________________________________________
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // for JBPS paper
            int k = (n + 2) * 10000;         // sequence size. Morris is (n+1)*10. sobol is (n+2)*100 or 10000
            string filepath = @"C:\Users\chwaibel\DATA\PROJEKTE\18_FitnessLandscapeAnalysis\04_CASESTUDY\TestFunctions_JBPS\Sobol\N40_input_large.csv";
            string outputpath = @"C:\Users\chwaibel\DATA\PROJEKTE\18_FitnessLandscapeAnalysis\04_CASESTUDY\TestFunctions_JBPS\Sobol\";

            tf.Add(SO.B_Sphere);     // 
            tf.Add(SO.L_Ackley);     // 
            tf.Add(SO.L_Rastrigin);    // 
            tf.Add(SO.V_Rosenbrock); // 
            double[] lb = new double[tf.Count];
            double[] ub = new double[tf.Count];
            for (int i = 0; i < tf.Count(); i++)
            {
                lb[i] = -5;
                ub[i] = 5;
            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //________________________________________________________________________________________________________





            Sequences.parameters param = new Sequences.parameters();


            // loading sequences (pre-defined)
            // and run on test functions
            double[][] x = Sequences.LoadInputSequence(n, k, filepath, param);
            Console.WriteLine(" ");


            //compute outputs for x for every test function.
            List<double[]> ytf = new List<double[]>();
            int counterr = 0;
            foreach (Func<double[], double> f in tf)
            {
                ytf.Add(new double[x.Length]);
                for (int j = 0; j < x.Length; j++)
                {
                    double[] xin = new double[n];
                    for (int i = 0; i < n; i++) xin[i] = x[j][i] * (ub[counterr] - lb[counterr]) + lb[counterr];
                    ytf[counterr][j] = f(xin);
                }
                counterr++;
            }


            //write outputs
            for (int f = 0; f < tf.Count; f++)
            {
                string[] stry = new string[k];
                for (int j = 0; j < k; j++)
                    stry[j] = ytf[f][j] + ";";

                Misc.IO.WriteTextFile(outputpath, "output_n" + n + "_tf" + f + ".csv", stry);

                //////MICHALEWICZ FUNCTION ONLY
                //writetextfile(outputpath, "output_n" + n + "_tf" + 4 + "_large.csv", stry);



                Console.WriteLine("tf {0} done", f);
            }


            Console.WriteLine("All Sampling done");
            Console.ReadKey();

        }


        /// <summary>
        /// Random Walks for Simulation Problems. 
        /// Creating sequences.
        /// Output needs to run in Grasshopper.
        /// But metric calculation again here.
        /// </summary>
        /// <param name="args"></param>
        static void RandomWalksSequenceCreation()
        {

            // n=4,10,11,13,17,18,20,35



            int n = 10;             // problem dimension
            int k = n * 100;          // sequence size
            int walks = 20;         // number of random walks
            Random rnd = new Random(42);
            string path = @"C:\temp\";

            // parameters for sequence generation
            Sequences.parameters param = new Sequences.parameters();
            param.distribution = "normal";
            param.stepsize = 0.02;
            param.verbose = false;




            // create sequences (random walks)
            // and run on test functions
            string[] strxAll = new string[k * walks];
            int counter = 0;
            for (int w = 0; w < walks; w++)
            {
                param.x0 = new double[n];
                param.seed = w;
                for (int i = 0; i < n; i++) param.x0[i] = rnd.NextDouble();
                double[][] x = Sequences.RandomWalk(n, k, param);

                for (int j = 0; j < k; j++)
                {
                    strxAll[counter] += x[j][0];
                    for (int i = 1; i < n; i++)
                    {
                        strxAll[counter] += ";" + x[j][i];
                    }
                    counter++;
                }
            }
            Misc.IO.WriteTextFile(path, "input_n" + n + "_allWalks.csv", strxAll);
        }


        /// <summary>
        /// Random Walks for Mathematical test functions
        /// </summary>
        /// <param name="args"></param>
        static void RandomWalksTestFuncs()
        {
            int n = 20;             // problem dimension
            int k = n * 100;          // sequence size
            int walks = 20;         // number of random walks
            Random rnd = new Random(42);
            string path = @"C:\";



            List<Func<double[], double>> tf = new List<Func<double[], double>>();
            tf.Add(SO.B_Sphere);     // [-1, 1] for all x
            tf.Add(SO.L_Ackley);     // [-32.768, 32.768] for all x
            tf.Add(SO.L_Rastrigin);    // [-5.12, 5.12] for all x
            tf.Add(SO.V_Rosenbrock); // [-2.048, 2.048] for all x
            double[] lb = new double[tf.Count];
            double[] ub = new double[tf.Count];
            lb[0] = -1;
            lb[1] = -32.768;
            lb[2] = -5.12;
            lb[3] = -2.048;
            ub[0] = 1;
            ub[1] = 32.768;
            ub[2] = 5.12;
            ub[3] = 2.048;

            //tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.C_MichalewiczSchoenauer); // only for n=13. domain: [0,1] for i=0-8 and 12; [0,100] for i=9-11
            //double[] lb = new double[13];
            //double[] ub = new double[13];
            //for (int i = 0; i < 9; i++)
            //{
            //    lb[i] = 0;
            //    ub[i] = 1;
            //}
            //lb[12] = 0;
            //ub[12] = 1;
            //for (int i = 9; i < 12; i++)
            //{
            //    lb[i] = 0;
            //    ub[i] = 100;
            //}






            // parameters for sequence generation
            Sequences.parameters param = new Sequences.parameters();
            param.distribution = "normal";
            param.stepsize = 0.02;
            param.verbose = false;




            // create sequences (random walks)
            // and run on test functions
            for (int w = 0; w < walks; w++)
            {
                param.seed = w;
                param.x0 = new double[n];
                for (int i = 0; i < n; i++) param.x0[i] = rnd.NextDouble();
                double[][] x = Sequences.RandomWalk(n, k, param);
                Console.WriteLine(" ");

                string[] strx = new string[k];
                for (int j = 0; j < k; j++)
                    for (int i = 0; i < n; i++)
                        strx[j] += x[j][i] + ";";
                Misc.IO.WriteTextFile(path, "input_n" + n + "_" + w + ".txt", strx);



                //compute outputs for x for every test function.
                List<double[]> ytf = new List<double[]>();
                int counterr = 0;
                foreach (Func<double[], double> f in tf)
                {
                    ytf.Add(new double[x.Length]);
                    for (int i = 0; i < x.Length; i++)
                    {
                        double[] xin = new double[n];
                        for (int nn = 0; nn < n; nn++)
                        {
                            // NOT FOR MICHALEWICZ FUNCTION
                            xin[nn] = (x[i][nn] * (ub[counterr] - lb[counterr])) + lb[counterr];

                            //// ONLY MICHALEWICZ
                            //xin[nn] = (x[i][nn] * (ub[nn] - lb[nn])) + lb[nn];
                        }

                        ytf[counterr][i] = f(xin);
                    }
                    counterr++;
                }


                //write outputs
                for (int f = 0; f < tf.Count; f++)
                {
                    string[] stry = new string[k];
                    for (int j = 0; j < k; j++)
                        stry[j] = ytf[f][j] + ";";

                    Misc.IO.WriteTextFile(path, "output_n" + n + "_tf" + f + "_walk" + w + ".txt", stry);

                    //////MICHALEWICZ FUNCTION ONLY
                    //writetextfile(path, "output_n" + n + "_tf" + 4 + "_walk" + w + ".txt", stry);
                }


                Console.WriteLine("walk {0} done", w);
            }





            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
