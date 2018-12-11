using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessLandscapeAnalysis
{


    /// <summary>
    /// Functions to create sampling sequences for Static Fitness Landscape Analysis
    /// </summary>
    public static class Sequences
    {

        /// <summary>
        /// Parameters for generating sequences.
        /// </summary>
        public struct parameters
        {
            /// <summary>
            /// Optional. Sampling mode. Possible arguments (1 is default): (1) hypercube; (2) openball; 
            /// </summary>
            public string domainmode;

            /// <summary>
            /// Optional. Sampling distribution. Possible arguments (1 is default): (1) uniform; (2) normal;
            /// </summary>
            public string distribution;

            /// <summary>
            /// Optional. Initial point. Default is at the domain center, i.e. 0.5 for each i in n.
            /// </summary>
            public double[] x0;

            /// <summary>
            /// Optional. Radius of the open ball domain. Default is 0.25. If an x0 is chosen, such that the ball exceeds or touches the boundaries (i.e. "&gt;=" 1), then x0 is automatically shifted inwards, such that the condition of an open ball is fulfilled.
            /// </summary>
            public double? ballradius;

            /// <summary>
            /// Seed for pseudo random number generator. Default is 42
            /// </summary>
            public int? seed;

            /// <summary>
            /// Constant stepsize "&gt;" 0 and "&lt;" 1. A value of 0.01 is recommended for a uniform distribution.
            /// </summary>
            public double? stepsize;

            /// <summary>
            /// Print details in console?
            /// </summary>
            public bool verbose;
        }




        /// <summary>
        /// Creates a multivariate random walk sequence, based on random vectors of constant length.
        /// Vectors are rotated for each step using Gram-Schmidt Orthogonalization, where the
        /// base vector is created from the current point to a random chosen point.
        /// Sequence assumes normalized space.
        /// </summary>
        /// <param name="n">Dimension.</param>
        /// <param name="k">Cardinality of the sequence.</param>
        /// <returns>k x n matrix with k samples of n-dimensional vectors.</returns>
        public static double[][] RandomWalk(int n, int k, parameters param)
        {
            if (String.IsNullOrEmpty(param.domainmode)) param.domainmode = "hypercube";
            if (String.IsNullOrEmpty(param.distribution)) param.distribution = "uniform";
            if (param.ballradius == null) param.ballradius = 0.25;
            if (param.seed == null) param.seed = 42;
            if (param.stepsize == null) param.stepsize = 0.05;
             //if (param.verbose == null) param.verbose = false;

            Misc.RandomDistributions rnd = new Misc.RandomDistributions(param.seed.Value);


            if (param.x0 == null)
            {
                param.x0 = new double[n];
                for (int i = 0; i < n; i++)
                {
                    param.x0[i] = 0.5;
                }
            }

            if (param.x0.Length != n) return null;


            double[][] x = new double[k][];
            x[0] = new double[n];
            param.x0.CopyTo(x[0], 0);

            for (int j = 1; j < k; j++)
            {
                x[j] = new double[n];
                double[] r = new double[n];

                if (String.Equals(param.domainmode, "hypercube"))
                {
                    if (string.Equals(param.distribution, "uniform"))
                    {
                        for (int i = 0; i < n; i++)
                        {
                            r[i] = rnd.NextDouble() - 0.5;      //random number between -0.5 and 0.5
                        }
                    }
                    else
                    {
                        for (int i = 0; i < n; i++)
                        {
                            r[i] = rnd.NextGaussian(0,1);      //random number normally distributed with stdev 1
                        }
                    }

                    double[] xt = new double[n];
                    double normr = Misc.Vector.Norm(r);
                    for (int i = 0; i < n; i++)
                    {
                        xt[i] = r[i] / normr * param.stepsize.Value;    // normalize vector and scale with stepsize
                        x[j][i] = x[j - 1][i] + xt[i];      // add new point by adding new vector with previous point
                        if (x[j][i] < 0) x[j][i] = x[j - 1][i] - xt[i];
                        else if (x[j][i] > 1) x[j][i] = x[j - 1][i] - xt[i];
                    }
                }
            }


            if (param.verbose)
            {
                for (int j = 0; j < k; j++)
                {
                    string str = Convert.ToString(x[j][0]);
                    for (int i = 1; i < n; i++) str += "," + x[j][i];
                    Console.WriteLine(str);
                }
            }

            return x;
        }



        /// <summary>
        /// Load a pre-defined input sequence from a csv file. Each row one sample, variables separated with comma ','.
        /// </summary>
        /// <param name="n">Dimension.</param>
        /// <param name="k">Cardinality of the sequence.</param>
        /// <param name="PathInput">Path of input sequence. (E.g. 'C:\\sequence.csv')</param>
        /// <param name="param"></param>
        /// <returns>k x n matrix with k samples of n-dimensional vectors.</returns>
        public static double[][] LoadInputSequence(int n, int k, string PathInput, parameters param)
        {
            //load input sequence for sampling
            List<double[]> xlist = new List<double[]>();
            System.IO.StreamReader file = new System.IO.StreamReader(PathInput);
            string line;
            string[] text = new string[] { };
            int linecount = 0;
            while ((line = file.ReadLine()) != null)
            {
                xlist.Add(new double[n]);
                text = line.Split(',');
                int counter = 0;
                foreach (string t in text)
                {
                    xlist[linecount][counter] = Convert.ToDouble(t);
                    counter++;
                }
                linecount++;
            }
            file.Close();


            double[][] x = xlist.ToArray();
            if (param.verbose)
            {
                for (int j = 0; j < k; j++)
                {
                    string str = Convert.ToString(x[j][0]);
                    for (int i = 1; i < n; i++) str += "," + x[j][i];
                    Console.WriteLine(str);
                }
            }

            return x;
        }



    }
}
