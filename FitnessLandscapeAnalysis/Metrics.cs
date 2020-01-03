using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessLandscapeAnalysis
{
    /// <summary>
    /// Computes Fitness Landscape metrics
    /// </summary>
    public static class Metrics
    {
        /// <summary>
        /// Fitness Distance Correlation.
        /// Global metric. Can be used as both dynamic (also: exploratory) and static FLA.
        /// Paper source:
        /// Jones, Terry, and Stephanie Forrest. 1995. 
        /// Fitness Distance Correlation as a Measure ofProblem Difficulty for Genetic Algorithms.
        /// In: Proceedings of the 6th International Conference on Genetic Algorithms, Pittsburgh, PA, Vol. 95, 184–192.
        /// Also:
        /// Pitzer, Erik, and Michael Affenzeller. 2012. 
        /// A Comprehensive Survey on Fitness Landscape Analysis.
        /// In: RecentAdvances in IntelligentEngineering Systems, 
        /// edited by János Fodor, Ryszard Klempous, and Carmen Paz Suárez Araujo, 161–191. Berlin: Springer.
        /// </summary>
        /// <param name="X">parameters of input sequence</param>
        /// <param name="y">cost values of input sequence</param>
        /// <param name="xlb">lower bound of parameters</param>
        /// <param name="xub">upper bound of parameters</param>
        /// <param name="ymin">value of cost minimal solution</param>
        /// <param name="xmin">parameters of cost minimal solution</param>
        /// <param name="distancem_measure">distance measure to compute state distance. Options: "Euclidean". to do: Hamming, etc.</param>
        /// <returns></returns>
        public static double FDC(double [][] X, double [] y, double [] xlb, double [] xub,
            double? ymin = null, double [] xmin = null, string distancem_measure = "Euclidean")
        {
            int n = X[0].Length;    // problem dimension
            int P = X.Length;       // population size

            if (ymin == null || xmin == null)
            {
                int ymin_index = 0;
                double ymin_temp = double.MaxValue;
                for (int i = 0; i < y.Length; i++)
                {
                    if (y[i] < ymin_temp)
                    {
                        ymin = y[i];
                        ymin_index = i;
                    }
                }
                xmin = new double[X[0].Length];
                X[ymin_index].CopyTo(xmin, 0);
            }

            // normalize all cost values
            for (int i = 0; i < n; i++)
            {
                xmin[i] = (xmin[i] - xlb[i]) / (xub[i] - xlb[i]);
                for (int j = 0; j < P; j++)
                {
                    X[j][i] = (X[j][i] - xlb[i]) / (xub[i] - xlb[i]);
                }
            }
            
            // calculate state distance dj to global optimum point using Euclidean Distance
            // TO DO !!! other distance measures, like Hamming
            double[] d = new double[P];
            for (int j = 0; j < P; j++)
            {
                d[j] = 0.0;
                for (int i = 0; i < n; i++)
                {
                    d[j] = d[j] + Math.Sqrt(Math.Pow(X[j][i] - xmin[i], 2));
                }
            }

            // calculate mean y and mean d, and stdev y and d
            double dmean = Misc.Statistics.Mean(d);
            double ymean = Misc.Statistics.Mean(y);
            double sigma_d = Misc.Statistics.Stdev(d);
            double sigma_f = Misc.Statistics.Stdev(y);

            // FDC value: see Pitzer2012, Eq. 8.13
            double fdc = 0.0;
            double sumfd = 0.0;
            for (int j = 0; j < P; j++)
            {
                sumfd += (y[j] - ymean) * (d[j] - dmean);
            }
            fdc = (sumfd / (double)P) / (sigma_f * sigma_d);
            return fdc;
        }

        // State Distribution
        //      average gradient?, mean distance


        // Fitness Distribution
        //      mean, variance, skeweness, kurtosis


        // Sobol Indices
        //      LTi, Li





        // Entropy



        // Ruggedness


        // Box-Jenkins...



    }
}
