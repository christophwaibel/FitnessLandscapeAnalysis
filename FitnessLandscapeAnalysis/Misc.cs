using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessLandscapeAnalysis.Misc
{
    /// <summary>
    /// Statistical measures
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// Returns the mean of a multivariate distribution
        /// </summary>
        /// <param name="X">Multivariate distribution. First index contains the samples, second index the paramaters.</param>
        /// <param name="subsample">If true, subtract 1 from population size</param>
        /// <returns>Multivariate mean</returns>
        public static double[] Mean(double[][] X, bool subsample = false)
        {
            int n = X[0].Length;
            int P = X.Length;
            int Pdiv = P;
            if (subsample) Pdiv = Pdiv - 1;

            double[] xmean = new double[n];
            for (int i = 0; i < n; i++)
            {
                xmean[i] = 0.0;
                for (int j = 0; j < P; j++)
                {
                    xmean[i] += X[j][i];
                }
                xmean[i] /= Pdiv;
            }

            return xmean;
        }


        /// <summary>
        /// Returns the mean of a univariate distribution
        /// </summary>
        /// <param name="X">Distribution</param>
        /// <param name="subsample">If ture, substract 1 from population size</param>
        /// <returns>Mean of distribution</returns>
        public static double Mean(double[] X, bool subsample = false)
        {
            double[][] X_in = new double[1][];
            X_in[0] = new double[X.Length];
            X.CopyTo(X_in[0], 0);

            double[] xmean_out = Mean(X_in, subsample);
            return xmean_out[0];
        }


        /// <summary>
        /// Computes the standard deviation of a multivariate distribution
        /// </summary>
        /// <param name="X">Multivariate distribution</param>
        /// <param name="subsample">If ture, substract 1 from population size</param>
        /// <param name="xmean">mean of distribution, if known (saves some computation time)</param>
        /// <returns>Multivariate standard deviation</returns>
        public static double[] Stdev(double[][] X,
            bool subsample = false, double[] xmean = null)
        {
            int n = X[0].Length;
            int P = X.Length;
            int Pdiv = P;
            if (subsample) Pdiv = Pdiv - 1;

            if (xmean == null) xmean = Mean(X, subsample);

            double[] stdev = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sumP = 0.0;
                for (int j = 0; j < P; j++)
                {
                    sumP += Math.Pow(X[j][i] - xmean[i], 2);
                }
                stdev[i] = Math.Sqrt((1 / Pdiv) * sumP);
            }

            return stdev;
        }


        /// <summary>
        /// Computes the standard deviation of a univariate distribution
        /// </summary>
        /// <param name="X">Distribution</param>
        /// <param name="subsample">If ture, substract 1 from population size</param>
        /// <param name="xmean">Mean of distribution, if known (saves computation)</param>
        /// <returns>Standard deviation</returns>
        public static double Stdev(double[] X,
            bool subsample = false, double? xmean = null)
        {
            double[][] X_in = new double[1][];
            X_in[0] = new double[X.Length];
            X.CopyTo(X_in[0], 0);
            double[] stdev_out = Stdev(X_in, subsample);
            return stdev_out[0];
        }
    }


    /// <summary>
    /// Additional random distributions. 
    /// </summary>
    public class RandomDistributions : Random
    {

        public RandomDistributions(int rndSeed)
            : base(rndSeed)
        { }

        /// <summary>
        /// Normal distributed random number.
        /// </summary>
        /// <param name="mean">Mean of the distribution.</param>
        /// <param name="stdDev">Standard deviation of the distribution.</param>
        /// <returns>Normal distributed random number.</returns>
        public double NextGaussian(double mean, double stdDev)
        {
            //Random rand = new Random(); //reuse this if you are generating many
            double u1 = base.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = base.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;

        }

        /// <summary>
        /// Normal distributed random number, normalized between 0 and 1. Assuming range: -5 to +5.
        /// </summary>
        /// <param name="mean">Mean of the distribution.</param>
        /// <param name="stdDev">Standard deviation of the distribution.</param>
        /// <returns>Normal distributed random number, normalized between 0 and 1.</returns>
        public double NextGaussianNorm(double mean, double stdDev)
        {
            double gauss = this.NextGaussian(mean, stdDev);
            if (gauss < -5) gauss = -5;
            else if (gauss > 5) gauss = 5;
            return (gauss + 5) / 10;
        }

    }

    /// <summary>
    /// Vector Operations.
    /// </summary>
    public static class Vector
    {
        public static double Norm(double[] x)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += Math.Pow(x[i], 2);
            }

            return Math.Sqrt(sum);

        }

        /// <summary>
        /// Computes the centroid of points
        /// </summary>
        /// <param name="X">Input points</param>
        /// <returns>Centroid</returns>
        public static double[] Centroid(double[][] X)
        {
            double[] centroid = new double[X[0].Length];
            for (int i = 0; i < X[0].Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < X.Length; j++)
                {
                    sum += X[j][i];
                }
                centroid[i] = sum / X.Length;
            }
            return centroid;
        }

    }



    /// <summary>
    /// Matrix Operations.
    /// Source: www.kniaz.net
    /// </summary>
    public static class MatrixKniaz
    {
        /// <summary>
        /// Generates n x m jagged array of doubles
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[][] New(int m, int n)
        {
            double[][] A = new double[m][];
            for (int i = 0; i < m; i++)
            {
                A[i] = new double[n];
            }

            return A;
        }





        /// <summary>
        /// Sqr Root of the sum column sqrs
        /// </summary>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Norma(int k, int l, double[][] a)
        {
            int j;
            double c;
            c = 0;
            for (j = 0; j < k; j++)
            {
                c += a[l][j] * a[l][j];
            }
            return (System.Math.Sqrt(c));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="a"></param>
        public static void Wers(int k, int l, double[][] a)
        {
            int i;
            double c;
            c = MatrixKniaz.Norma(k, l, a);
            for (i = 0; i < k; i++)
                a[l][i] = a[l][i] / c;
        }

        /// <summary>
        /// Scalar Product of two matrices
        /// </summary>
        /// <param name="k"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double scalarProduct(int k, int p, int q, double[][] a)
        {
            int i;
            double skal;
            skal = 0;
            for (i = 0; i < k; i++)
                skal += a[p][i] * a[q][i];
            return (skal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wym">dimensions</param>
        /// <param name="s1">basevector, used for rotation</param>
        /// <param name="a">current orthogonal vectors</param>
        public static void Minim(int wym, double[] s1, double[][] a)
        {
            double c1;
            double[][] b = MatrixKniaz.New(wym, wym);
            int i, j, k;
            for (i = 0; i < wym; i++)
                for (j = 0; j < wym; j++)
                {
                    c1 = 0;
                    for (k = i; k < wym; k++) c1 += s1[k] * a[k][j];
                    b[i][j] = c1;
                }
            for (i = 0; i < wym; i++)
                for (j = 0; j < wym; j++) a[i][j] = b[i][j];
        }



        /// <summary>
        /// Performs Orthogonalization of the vector
        /// </summary>
        /// <param name="wym">dimensions</param>
        /// <param name="v">current orthogonal vectors, after "minim" was run with basevector</param>
        public static void Ortog(int wym, double[][] v)
        {
            int i, j, k;
            double lask = 0.0;
            j = 0;

            double[] w1 = new double[wym];

            MatrixKniaz.Wers(wym, j, v);
            for (i = 1; i < wym; i++)
            {
                for (k = 0; k < wym; k++) w1[k] = 0;
                for (j = 0; j <= i - 1; j++)
                {
                    lask = MatrixKniaz.scalarProduct(wym, i, j, v);
                    for (k = 0; k < wym; k++) w1[k] += v[j][k] * lask;
                }
                for (k = 0; k < wym; k++) v[i][k] += -w1[k];
                MatrixKniaz.Wers(wym, i, v);
            }
        }


    }


    public static class Transformation
    {
        public static double[][] GramSchmidt(double[] basevector, int nVar, double[][] vMoves)
        {
            double[][] rotatedMatrix = new double[nVar][];
            for (int i = 0; i < nVar; i++)
            {
                rotatedMatrix[i] = new double[nVar];
                rotatedMatrix[i] = vMoves[i];
            }

            MatrixKniaz.Minim(nVar, basevector, rotatedMatrix);
            MatrixKniaz.Ortog(nVar, rotatedMatrix);

            return rotatedMatrix;
        }

    }
}
