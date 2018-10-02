using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FitnessLandscapeAnalysis
{
    class Program
    {


        /// <summary>
        /// Random Walks for BEO Problems. 
        /// Creating sequences.
        /// Output needs to run in Grasshopper.
        /// But metric calculation again here.
        /// </summary>
        /// <param name="args"></param>
        static void BEOMain(string[] args)
        {

            // n=4,10,11,13,17,18,20,35



            int n = 4;             // problem dimension
            int k = n*100;          // sequence size
            int walks = 20;         // number of random walks
            Random rnd = new Random(42);
            string path = @"C:\";

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
            writetextfile(path, "input_n" + n + "_allWalks.csv", strxAll);
        }




        /// <summary>
        /// Random Walks for Mathematical test functions
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 20;             // problem dimension
            int k = n*100;          // sequence size
            int walks = 20;         // number of random walks
            Random rnd = new Random(42);
            string path = @"C:\";



            List<Func<double[], double>> tf = new List<Func<double[], double>>();
            tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.B_Sphere);     // [-1, 1] for all x
            tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.L_Ackley);     // [-32.768, 32.768] for all x
            tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.L_Rastrigin);    // [-5.12, 5.12] for all x
            tf.Add(FitnessLandscapeAnalysis.Testfunctions.SO.V_Rosenbrock); // [-2.048, 2.048] for all x
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
                writetextfile(path, "input_n" + n + "_" + w + ".txt", strx);



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

                    writetextfile(path, "output_n" + n + "_tf" + f + "_walk" + w + ".txt", stry);

                    //////MICHALEWICZ FUNCTION ONLY
                    //writetextfile(path, "output_n" + n + "_tf" + 4 + "_walk" + w + ".txt", stry);
                }
              

                Console.WriteLine("walk {0} done", w);
            }

            


            
            Console.WriteLine("Done");
            Console.ReadKey();
        }




        static void writetextfile(string path, string filename, string [] textperline)
        {
            using (FileStream fs = new FileStream(path + filename, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
                foreach (string str in textperline)
                    sw.WriteLine(str);
        }
    }
}
