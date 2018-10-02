// !!!!!!!
// -change documentation, using xml stuff, for preview
// -MO2_ZDT5 missing 
// -many more functions in the bottom to do
// - using interfaces ISingleEvalFunc, IMultiEvalFunc... meaning I can't use static classes and static functions anymore? does it make sense? check later
// - check COCO platform, which testfunctions to add http://coco.gforge.inria.fr/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessLandscapeAnalysis.Testfunctions
{
    /// <summary>
    ///  Repository of benchmark functions for single objective optimization.</summary>
    ///     <remarks> Types: 
    ///     <para/>- L: many local minima, 
    ///     <para/>- B: bowl shaped,
    ///     <para/>- P: plate shaped,
    ///     <para/>- V: valley shaped,
    ///     <para/>- S: steep ridges or drops,
    ///     <para/>- O: other.
    ///     <para/>(Web-)Sources:
    ///     <para/>http://www.sfu.ca/~ssurjano/optimization.html
    ///     <para/>https://en.wikipedia.org/wiki/Test_functions_for_optimization </remarks>
    public static class SO
    {

        #region  ManyLocalMinima
        /// <summary>
        /// ACKLEY Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// <para/> Description: The Ackley function is widely used for testing optimization algorithms. In its two-dimensional form, it is characterized by a nearly flat outer region, and a large hole at the centre. The function poses a risk for optimization algorithms, particularly hillclimbing algorithms, to be trapped in one of its many local minima.
        /// </summary>
        /// <param name="x">Decision variable vector. Usually: xi ∈ [-32.768, 32.768], or [-5, 5], for all i = 1, …, d.</param>
        /// <param name="a">(optional) parameter. Default 20.</param>
        /// <param name="b">(optional) parameter. Default 0.2.</param>
        /// <param name="c">(optional) parameter. Default 2PI.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0).</returns>
        public static double L_Ackley(double[] x)
        {
            double a = 20;
            double b = 0.2;
            double c = (2 * Math.PI);
            double term1, term2, xi, sum1 = 0, sum2 = 0;
            int d = x.Length;

            for (int i = 0; i < d; i++)
            {
                xi = x[i];
                sum1 += (Math.Pow(xi, 2));
                sum2 += (Math.Cos(c * xi));
            }

            term1 = -a * Math.Exp(-b * Math.Sqrt(sum1 / d));
            term2 = -Math.Exp(sum2 / d);

            return (term1 + term2 + a + Math.Exp(1));
        }
        //public static double L_Ackley(double[] x, double a = 20, double b = 0.2, double c = (2*Math.PI ))
        //{
        //    double term1, term2, xi, sum1 = 0, sum2 = 0;
        //    int d = x.Length;
        //    Console.WriteLine(d.ToString());

        //    for (int i = 0; i < d; i++)
        //    {
        //        xi = x[i];
        //        sum1 += (Math.Pow(xi, 2));
        //        sum2 += (Math.Cos(c * xi));
        //    }

        //    term1 = -a * Math.Exp(-b * Math.Sqrt(sum1 / d));
        //    term2 = -Math.Exp(sum2 / d);

        //    return (term1 + term2 + a + Math.Exp(1));
        //}


        /// <summary>
        /// BUKIN 6 Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// <para/> Description: The sixth Bukin function has many local minima, all of which lie in a ridge. 
        /// </summary>
        /// <param name="x">Decision variable vector. Usually: x1 ∈ [-15, -5], x2 ∈ [-3, 3].</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (-10, 1).</returns>
        public static double L_Bukin6(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = 100 * Math.Sqrt(Math.Abs(x2 - 0.01 * Math.Pow(x1, 2)));
            double term2 = 0.01 * Math.Abs(x1 + 10);

            return (term1 + term2);
        }

        /// <summary>
        /// CrossInTray Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// </summary>
        /// <param name="x">Decision variable vector. Usually: xi ∈ [-10, 10], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -2.06261, at x* = (1.3491, -1.3491), (1.3491, 1.3491), (-1.3491, 1.3491) and (-1.3491, -1.3491).</returns>
        public static double L_CrossInTray(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double fact1 = Math.Sin(x1) * Math.Sin(x2);
            double fact2 = Math.Exp(Math.Abs(100 - Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(x2, 2)) / Math.PI));

            return (-0.0001 * Math.Pow((Math.Abs(fact1 * fact2) + 1), 0.1));
        }

        /// <summary>
        /// DROP-WAVE Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// </summary>
        /// <param name="x">Decision variable vector. Usually: xi ∈ [-5.12, 5.12], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -1, at x* = (0, 0).</returns>
        public static double L_DropWave(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double fract1 = 1 + Math.Cos(12 * Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(x2, 2)));
            double fract2 = 0.5 * (Math.Pow(x1, 2) + Math.Pow(x2, 2)) + 2;

            return (-fract1 / fract2);
        }

        /// <summary>
        /// EGGHOLDER Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// </summary>
        /// <param name="x">Decision variable vector. Usually: xi ∈ [-512, 512], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*)= -959.6407, at x* = (512, 404.2319).</returns>
        public static double L_Eggholder(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = -(x2 + 47) * Math.Sin(Math.Sqrt(Math.Abs(x2 + x1 / 2 + 47)));
            double term2 = -x1 * Math.Sin(Math.Sqrt(Math.Abs(x1 - (x2 + 47))));

            return (term1 + term2);
        }

        /// <summary>
        /// GRAMACY-LEE Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 1.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// <para/> Description: This is a simple one-dimensional test function. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: x ∈ [0.5, 2.5]</param>
        /// <returns>Objective function value f(x). Global minimum f(x*)= ?, at x* = ?</returns>
        public static double L_GramacyLee(double x)
        {

            double term1 = Math.Sin(10 * Math.PI * x) / (2 * x);
            double term2 = Math.Pow((x - 1), 4);

            return (term1 + term2);
        }

        /// <summary>
        /// GRIEWANK Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// <para/> Description: The Griewank function has many widespread local minima, which are regularly distributed.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-600, 600], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0).</returns>
        public static double L_Griewank(double[] x)
        {
            int d = x.Length;
            double sum = 0;
            double prod = 1;

            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                sum += Math.Pow(xi, 2) / 4000;
                prod *= Math.Cos(xi / Math.Sqrt(i + 1));
            }

            return (sum - prod + 1);
        }

        /// <summary>
        /// HOLDER TABLE Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// <para/> Description: The Holder Table function has many local minima, with four global minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -19.2085, at x* = (8.05502, 9.66459), (8.05502, -9.66459), (-8.05502, 9.66459), (-8.05502, -9.66459).</returns>
        public static double L_HolderTable(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double fact1 = Math.Sin(x1) * Math.Cos(x2);
            double fact2 = Math.Exp(Math.Abs(1 - Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(x2, 2)) / Math.PI));

            return (-Math.Abs(fact1 * fact2));
        }

        /// <summary>
        /// LANGERMANN Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// <para/> Description: The Langermann function is multimodal, with many unevenly distributed local minima.
        /// <para/> Recommended values of m, c and A, are given by Molga &amp; Smutnicki (2005) for d = 2.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [0, 10], for all i = 1, …, d.</param>
        /// <param name="m">(optional) parameter. Default 5 (for d = 2).</param> 
        /// <param name="c">(optional) parameter. Default {1, 2, 5, 2, 3} (for d = 2).</param>
        /// <param name="A">(optional> parameter. Default A[5][2] = {3,5},{5,2},{2,1},{1,4},{7,9} (for d = 2).</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = ?, at x* = ?</returns> 
        public static double L_Langermann(double[] x)
        {
            double m = 5;
            double[] c = null;
            double[][] A = null;

            int d = x.Length;

            if (c == null && m == 5) c = new double[] { 1, 2, 5, 2, 3 };
            if (A == null && m == 5 && d == 2)
                A = new double[][]{
                    new double []{3,5}, 
                    new double []{5,2},
                    new double []{2,1}, 
                    new double []{1,4}, 
                    new double []{7,9}
                };

            if (m != c.Length) return 99999999999999;
            if (d != A[0].Length) return 999999999999;

            double outer = 0, newval;

            for (int i = 0; i < m; i++)
            {
                double inner = 0;
                for (int j = 0; j < d; j++)
                {
                    double xj = x[j];
                    double Aij = A[i][j];
                    inner += Math.Pow((xj - Aij), 2);
                }
                newval = c[i] * Math.Exp(-inner / Math.PI) * Math.Cos(Math.PI * inner);
                outer += newval;
            }

            return outer;
        }
        public static double L_Langermann(double[] x, int m, double[] c, double[][] A)
        {
            //double m = 5;
            //double[] c = null;
            //double[][] A = null;

            int d = x.Length;

            if (c == null && m == 5) c = new double[] { 1, 2, 5, 2, 3 };
            if (A == null && m == 5 && d == 2)
                A = new double[][]{
                    new double []{3,5}, 
                    new double []{5,2},
                    new double []{2,1}, 
                    new double []{1,4}, 
                    new double []{7,9}
                };

            if (m != c.Length) return 99999999999999;
            if (d != A[0].Length) return 999999999999;

            double outer = 0, newval;

            for (int i = 0; i < m; i++)
            {
                double inner = 0;
                for (int j = 0; j < d; j++)
                {
                    double xj = x[j];
                    double Aij = A[i][j];
                    inner += Math.Pow((xj - Aij), 2);
                }
                newval = c[i] * Math.Exp(-inner / Math.PI) * Math.Cos(Math.PI * inner);
                outer += newval;
            }

            return outer;
        }


        /// <summary>
        /// LEVY Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], for all i = 1, …, d. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (1, ..., 1).</returns> 
        public static double L_Levy(double[] x)
        {
            int d = x.Length;

            double[] w = new double[d];
            for (int i = 0; i < d; i++)
            {
                w[i] = 1 + (x[i] - 1) / 4;
            }

            double term1 = Math.Pow((Math.Sin(Math.PI * w[0])), 2);
            double term3 = Math.Pow((w[d - 1] - 1), 2) * Math.Pow((1 + (Math.Sin(2 * Math.PI * w[d - 1]))), 2);

            double sum = 0;

            for (int i = 0; i < d - 1; i++)
            {
                double wi = w[i];
                double newval = Math.Pow((wi - 1), 2) * (1 + 10 * Math.Pow((Math.Sin(Math.PI * wi + 1)), 2));
                sum += newval;
            }

            return (term1 + sum + term3);
        }

        /// <summary>
        /// LEVY 13 Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], for all i = 1, 2. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (1, 1).</returns> 
        public static double L_Levy13(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = Math.Pow((Math.Sin(3 * Math.PI * x1)), 2);
            double term2 = Math.Pow((x1 - 1), 2) * (1 + Math.Pow((Math.Sin(3 * Math.PI * x2)), 2));
            double term3 = Math.Pow((x2 - 1), 2) * (1 + Math.Pow((Math.Sin(2 * Math.PI * x2)), 2));

            return (term1 + term2 + term3);
        }

        /// <summary>
        /// RASTRIGIN Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// <para/> Description: The Rastrigin function has several local minima. It is highly multimodal, but locations of the minima are regularly distributed.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-5.12, 5.12], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0).</returns> 
        public static double L_Rastrigin(double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                sum += (Math.Pow(xi, 2) - 10 * Math.Cos(2 * Math.PI * xi));
            }

            return (10 * d + sum);
        }

        /// <summary>
        /// SCHAFFER N.2 Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-100, 100], for all i = 1, 2. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, 0).</returns> 
        public static double L_Schaffer2(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double fact1 = Math.Pow((Math.Sin(Math.Pow(x1, 2) - Math.Pow(x2, 2))), 2) - 0.5;
            double fact2 = Math.Pow((1 + 0.001 * (Math.Pow(x1, 2) + Math.Pow(x2, 2))), 2);

            return (0.5 + fact1 / fact2);
        }

        /// <summary>
        /// SCHAFFER N.4 Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-100, 100], for all i = 1, 2. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = ?, at x* = ?</returns> 
        public static double L_Schaffer4(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double fact1 = Math.Cos(Math.Sin(Math.Abs(Math.Pow(x1, 2) - Math.Pow(x2, 2)))) - 0.5;
            double fact2 = Math.Pow((1 + 0.001 * (Math.Pow(x1, 2) + Math.Pow(x2, 2))), 2);

            return (0.5 + fact1 / fact2);
        }

        /// <summary>
        /// SCHWEFEL Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-500, 500], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (420.9687, ..., 420.9687).</returns> 
        public static double L_Schwefel(double[] x)
        {
            int d = x.Length;
            double sum = 0;

            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                sum += (xi * Math.Sin(Math.Sqrt(Math.Abs(xi))));
            }

            return (418.9829 * d - sum);
        }

        /// <summary>
        /// SHUBERT Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Many local minima.
        /// <para/> Description: The Shubert function has several local minima and many global minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], or [-5.12, 5.12], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -186.7309, at x* = ?</returns> 
        public static double L_Shubert(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < 5; i++)
            {
                double new1 = (i + 1) * Math.Cos((i + 2) * x1 + (i + 1));
                double new2 = (i + 1) * Math.Cos((i + 2) * x2 + (i + 1));
                sum1 += new1;
                sum2 += new2;
            }

            return (sum1 * sum2);
        }

        #endregion








        #region BowlShaped
        /// <summary>
        /// BOHACHEVSKY N.1 Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0. 
        /// <para/> Type: Bowl-shaped.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-100, 100], for all i = 1, 2. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*)=0, at x*=(0,0).</returns> 
        public static double B_Bohachevsky1(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = Math.Pow(x1, 2);
            double term2 = 2 * Math.Pow(x2, 2);
            double term3 = -0.3 * Math.Cos(3 * Math.PI * x1);
            double term4 = -0.4 * Math.Cos(4 * Math.PI * x2);

            return (term1 + term2 + term3 + term4 + 0.7);
        }

        /// <summary>
        /// BOHACHEVSKY N.2 Function.
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0. 
        /// <para/> Type: Bowl-shaped.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-100, 100], for all i = 1, 2. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*)=0, at x*=(0,0).</returns> 
        public static double B_Bohachevsky2(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = Math.Pow(x1, 2);
            double term2 = 2 * Math.Pow(x2, 2);
            double term3 = -0.3 * Math.Cos(3 * Math.PI * x1) * Math.Cos(4 * Math.PI * x2);

            return (term1 + term2 + term3 + 0.3);
        }

        /// <summary>
        /// BOHACHEVSKY N.3 Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Type: Bowl-shaped.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-100, 100], for all i = 1, 2. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*)=0, at x*=(0,0).</returns> 
        public static double B_Bohachevsky3(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = Math.Pow(x1, 2);
            double term2 = 2 * Math.Pow(x2, 2);
            double term3 = -0.3 * Math.Cos(3 * Math.PI * x1 + 4 * Math.PI * x2);

            return (term1 + term2 + term3 + 0.3);
        }

        /// <summary>
        /// PERM 0,D,BETA Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Bowl-shaped.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-d, d], for all i = 1, …, d.</param>
        /// <param name="b">(optional) parameter. Default 10.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (1, 1/2, ..., 1/d).</returns> 
        public static double B_Perm0db(double[] x)
        {
            double b = 10;
            int d = x.Length;
            double outer = 0;

            for (int i = 1; i <= d; i++)
            {
                double inner = 0;
                for (int j = 1; j <= d; j++)
                {
                    double xj = x[j - 1];
                    inner += ((j + b) * (Math.Pow(xj, i) - Math.Pow((1 / j), i)));
                }
                outer += Math.Pow(inner, 2);
            }

            return outer;
        }
        //public static double B_Perm0db(double[] x, double b = 10)
        //{
        //    int d = x.Length;
        //    double outer = 0;

        //    for (int i = 1; i <= d; i++)
        //    {
        //        double inner = 0;
        //        for (int j = 1; j <= d; j++)
        //        {
        //            double xj = x[j - 1];
        //            inner += ((j + b) * (Math.Pow(xj, i) - Math.Pow((1 / j), i)));
        //        }
        //        outer += Math.Pow(inner, 2);
        //    }

        //    return outer;
        //}

        /// <summary>
        /// ROTATED HYPER-ELLIPSOID Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Bowl-shaped.
        /// <para/> Description: The Rotated Hyper-Ellipsoid function is continuous, convex and unimodal. It is an extension of the Axis Parallel Hyper-Ellipsoid function, also referred to as the Sum Squares function.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-65.536, 65.536], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0)</returns> 
        public static double B_RotHypEll(double[] x)
        {
            int d = x.Length;
            double outer = 0;

            for (int i = 0; i < d; i++)
            {
                double inner = 0;
                for (int j = 0; j <= i; j++)
                {
                    double xj = x[j];
                    inner += Math.Pow(xj, 2);
                }
                outer += inner;
            }

            return outer;
        }

        /// <summary>
        /// SPHERE Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Bowl-shaped.
        /// <para/> Description: The Sphere function has d local minima except for the global one. It is continuous, convex and unimodal.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-5.12, 5.12], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0)</returns> 
        public static double B_Sphere(double[] x)
        {
            int d = x.Length;
            double sum = 0;

            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                sum += Math.Pow(xi, 2);
            }

            return sum;
        }

        /// <summary>
        /// SPHERE Modified Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Bowl-shaped.
        /// <para/> Description: The Sphere function has d local minima except for the global one. It is continuous, convex and unimodal.
        /// <para/> This modified function has a mean of zero and a variance of one. The authors also add a small Gaussian error term to the output. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-5.12, 5.12], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0)</returns> 
        public static double B_SphereMod(double[] x)
        {
            double sum = 0;

            for (int i = 0; i < 6; i++)
            {
                double xi = x[i];
                sum += Math.Pow(xi, 2) * Math.Pow(2, (i + 1));
            }

            return ((sum - 1745) / 899);
        }

        /// <summary>
        /// SUM OF DIFFERENT POWERS Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Bowl-shaped.
        /// <para/> Description: The Sum of Different Powers function is unimodal.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-1, 1], for all i = 1, …, d. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0)</returns> 
        public static double B_SumDiffPow(double[] x)
        {
            int d = x.Length;
            double sum = 0;

            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                double newval = Math.Pow(Math.Abs(xi), (i + 2));
                sum += newval;
            }
            return sum;
        }

        /// <summary>
        /// SUM SQUARES Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Bowl-shaped.
        /// <para/> Description: The Sum Squares function, also referred to as the Axis Parallel Hyper-Ellipsoid function, has no local minimum except the global one. It is continuous, convex and unimodal.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], or [-5.12, 5.12] for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0)</returns> 
        public static double B_SumSquares(double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                sum += (i + 1) * Math.Pow(xi, 2);
            }

            return sum;
        }

        /// <summary>
        /// TRID Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Bowl-shaped.
        /// <para/> Description: The Trid function has no local minimum except the global one.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-d^2, d^2], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum at d = 6: f(x*) = -50; at d = 10: f(x*) = -200.</returns> 
        public static double B_Trid(double[] x)
        {
            int d = x.Length;
            double sum1 = Math.Pow(x[0] - 1, 2);
            double sum2 = 0;

            for (int i = 1; i < d; i++)
            {
                double xi = x[i];
                double xold = x[i - 1];
                sum1 += Math.Pow(xi - 1, 2);
                sum2 += xi * xold;
            }

            return (sum1 - sum2);
        }

        #endregion






        #region Plate-Shaped
        /// <summary>
        /// BOOTH Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Plate-shaped.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (1, 3).</returns> 
        public static double P_Booth(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            double term1 = Math.Pow((x1 + 2 * x2 - 7), 2);
            double term2 = Math.Pow((2 * x1 + x2 - 5), 2);

            return (term1 + term2);
        }

        /// <summary>
        /// MATYAS Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Plate-shaped.
        /// <para/> Description: The Matyas function has no local minima except the global one. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, 0).</returns> 
        public static double P_Matyas(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            double term1 = 0.26 * (Math.Pow(x1, 2) + Math.Pow(x2, 2));
            double term2 = -0.48 * x1 * x2;

            return (term1 + term2);
        }

        /// <summary>
        /// MCCORMICK Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Plate-shaped.
        /// </summary>
        /// <param name="x">Decision variable. Usually: x1 ∈ [-1.5, 4], x2 ∈ [-3, 4].</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -1.9133, at x* = (-0.54719, -1.54719).</returns> 
        public static double P_McCormick(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = Math.Sin(x1 + x2);
            double term2 = Math.Pow(x1 - x2, 2);
            double term3 = -1.5 * x1;
            double term4 = 2.5 * x2;

            return (term1 + term2 + term3 + term4 + 1);
        }

        /// <summary>
        /// POWER SUM Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Plate-shaped.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [0, d], for all i = 1, …, d.</param>
        /// <param name="b">(optional) parameter. Default {8, 18, 44, 114} (for d = 4).</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = ?, at x* = ?</returns> 
        public static double P_PowerSum(double[] x)
        {
            double[] b = null;
            int d = x.Length;
            if (b == null && d == 4)
            {
                b = new double[] { 8, 18, 44, 114 };
            }
            else if (b != null && b.Length != d || b == null && d != 4) return 9999999999999;

            double outer = 0;

            for (int i = 0; i < d; i++)
            {
                double inner = 0;
                for (int j = 0; j < d; j++)
                {
                    double xj = x[j];
                    inner += Math.Pow(xj, (i + 1));
                }
                outer += Math.Pow((inner - b[i]), 2);
            }

            return outer;
        }
        //public static double P_PowerSum(double[] x, double[] b = null)
        //{
        //    int d = x.Length;
        //    if (b == null && d == 4)
        //    {
        //        b = new double[] { 8, 18, 44, 114 };
        //    }
        //    else if (b != null && b.Length != d || b == null && d != 4) return 9999999999999;

        //    double outer = 0;

        //    for (int i = 0; i < d; i++)
        //    {
        //        double inner = 0;
        //        for (int j = 0; j < d; j++)
        //        {
        //            double xj = x[j];
        //            inner += Math.Pow(xj, (i + 1));
        //        }
        //        outer += Math.Pow((inner - b[i]), 2);
        //    }

        //    return outer;
        //}

        /// <summary>
        /// ZAKHAROV Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Plate-shaped.
        /// <para/> Description: The Zakharov function has no local minima except the global one.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-5, 10], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0).</returns> 
        public static double P_Zakharov(double[] x)
        {
            int d = x.Length;
            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                sum1 += Math.Pow(xi, 2);
                sum2 += 0.5 * (i + 1) * xi;
            }

            return (sum1 + Math.Pow(sum2, 2) + Math.Pow(sum2, 4));
        }

        #endregion






        #region Valley-Shaped
        /// <summary>
        /// THREE HUMP CAMEL Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Valley-shaped.
        /// <para/> Description: The function has three local minima. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-5, 5], for all i = 1, 2. </param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, 0).</returns> 
        public static double V_Camel3Hump(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = 2 * Math.Pow(x1, 2);
            double term2 = -1.05 * Math.Pow(x1, 4);
            double term3 = Math.Pow(x1, 6) / 6;
            double term4 = x1 * x2;
            double term5 = Math.Pow(x2, 2);

            return (term1 + term2 + term3 + term4 + term5);
        }

        /// <summary>
        /// SIX HUMP CAMEL Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Valley-shaped.
        /// <para/> Description: The function has six local minima, two of which are global. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: x1 ∈ [-3, 3], x2 ∈ [-2, 2].</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -1.0316, at x* = (0.0898, -0.7126) and (-0.0898, 0.7126).</returns> 
        public static double V_Camel6Hump(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double term1 = (4 - 2.1 * Math.Pow(x1, 2) + Math.Pow(x1, 4) / 3) * Math.Pow(x1, 2);
            double term2 = x1 * x2;
            double term3 = (-4 + 4 * Math.Pow(x2, 2)) * Math.Pow(x2, 2);

            return (term1 + term2 + term3);
        }

        /// <summary>
        /// DIXON PRICE Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Valley-shaped.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = 2^-((2^i - 2) / 2^i ), for i = 1, ..., d.</returns> 
        public static double V_DixonPrice(double[] x)
        {
            int d = x.Length;
            double x1 = x[0];
            double term1 = Math.Pow((x1 - 1), 2);

            double sum = 0;
            for (int i = 1; i < d; i++)
            {
                double xi = x[i];
                double xold = x[i - 1];
                double newval = (i + 1) * Math.Pow((2 * Math.Pow(xi, 2) - xold), 2);
                sum += newval;
            }

            return (term1 + sum);
        }

        /// <summary>
        /// ROSENBROCK Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Valley-shaped.
        /// <para/> Description: The Rosenbrock function, also referred to as the Valley or Banana function, is a popular test problem for gradient-based optimization algorithms. The function is unimodal, and the global minimum lies in a narrow, parabolic valley. However, even though this valley is easy to find, convergence to the minimum is difficult (Picheny et al., 2012). 
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-5, 10], or [-2.048, 2.048], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (1, ..., 1).</returns> 
        public static double V_Rosenbrock(double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d - 1; i++)
            {
                double xi = x[i];
                double xnext = x[i + 1];
                double newval = 100 * Math.Pow(xnext - Math.Pow(xi, 2), 2) + Math.Pow(xi - 1, 2);
                sum += newval;
            }

            return sum;
        }

        /// <summary>
        /// ROSENBROCK Rescaled Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Valley-shaped.
        /// <para/> Description: The Rosenbrock function, also referred to as the Valley or Banana function, is a popular test problem for gradient-based optimization algorithms. The function is unimodal, and the global minimum lies in a narrow, parabolic valley. However, even though this valley is easy to find, convergence to the minimum is difficult (Picheny et al., 2012). 
        /// <para/> Rescaled form of the function, with d = 4, on [0, 1]^2. This rescaled form of the function has a mean of zero and a variance of one. The authors also add a small Gaussian error term to the output
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-5, 10], or [-2.048, 2.048], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (1, ..., 1).</returns> 
        public static double V_RosenbrockRescaled(double[] x)
        {
            double[] xxbar = new double[4];
            for (int i = 0; i < 4; i++)
            {
                xxbar[i] = 15 * x[i] - 5;
            }

            double sum = 0;
            for (int i = 0; i < 3; i++)
            {
                double xibar = xxbar[i];
                double xnextbar = xxbar[i + 1];
                double newval = 100 * Math.Pow(xnextbar - Math.Pow(xibar, 2), 2) + Math.Pow(1 - xibar, 2);
                sum += newval;
            }

            return ((sum - 3.827 * Math.Pow(10, 5)) / (3.755 * Math.Pow(10, 5)));
        }

        #endregion





        #region SteepRidgesOrDrops
        /// <summary>
        /// DE JONG N.5 Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The fifth function of De Jong is multimodal, with very sharp drops on a mainly flat surface.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-65.536, 65.536], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = ?, at x* = ?.</returns> 
        public static double S_DeJong5(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            double sum = 0;

            double[,] A = new double[2, 25];
            double[] a = new double[] { -32, -16, 0, 16, 32 };
            for (int i = 0; i < 25; i = i + 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    A[0, j + i] = a[j];
                    A[1, j + i] = a[i / 5];
                }
            }

            for (int i = 0; i < 25; i++)
            {
                double a1i = A[0, i];
                double a2i = A[1, i];
                double term1 = i + 1;
                double term2 = Math.Pow(x1 - a1i, 6);
                double term3 = Math.Pow(x2 - a2i, 6);
                double newval = 1 / (term1 + term2 + term3);
                sum += newval;
            }

            return (1 / (0.002 + sum));
        }

        /// <summary>
        /// EASOM Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The Easom function has several local minima. It is unimodal, and the global minimum has a small area relative to the search space.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-100, 100], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -1, at x* = (Pi, Pi).</returns> 
        public static double S_Easom(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            double fact1 = -Math.Cos(x1) * Math.Cos(x2);
            double fact2 = Math.Exp(-Math.Pow(x1 - Math.PI, 2) - Math.Pow(x2 - Math.PI, 2));

            return (fact1 * fact2);
        }

        /// <summary>
        /// MICHALEWICZ Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The Michalewicz function has d! local minima, and it is multimodal. The parameter m defines the steepness of they valleys and ridges; a larger m leads to a more difficult search.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [0, π], for all i = 1, …, d.</param>
        /// <param name="m">(optional) parameter. Default m = 10.</param>
        /// <returns>Objective function value f(x). Global minimum at:  
        /// <para/>d = 2: f(x*) = -1.8011, at x* = (2.20, 1.57),
        /// <para/>d = 5: f(x*) = -4.687658,
        /// <para/>d = 10: f(x*)=-9.66015.</returns> 
        public static double S_Michalewicz(double[] x)
        {
            double m = 10;
            int d = x.Length;
            double sum = 0;

            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                double newval = Math.Sin(xi) * Math.Pow((Math.Sin((i + 1) * Math.Pow(xi, 2) / Math.PI)), (2 * m));
                sum += newval;
            }

            return -sum;
        }
        //public static double S_Michalewicz(double[] x, double m = 10)
        //{
        //    int d = x.Length;
        //    double sum = 0;

        //    for (int i = 0; i < d; i++)
        //    {
        //        double xi = x[i];
        //        double newval = Math.Sin(xi) * Math.Pow((Math.Sin((i + 1) * Math.Pow(xi, 2) / Math.PI)), (2 * m));
        //        sum += newval;
        //    }

        //    return -sum;
        //}

        #endregion







        #region Other
        /// <summary>
        /// BEALE Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The Beale function is multimodal, with sharp peaks at the corners of the input domain.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-4.5, 4.5], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (3, 0.5).</returns> 
        public static double O_Beale(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            double term1 = Math.Pow((1.5 - x1 + x1 * x2), 2);
            double term2 = Math.Pow((2.25 - x1 + x1 * Math.Pow(x2, 2)), 2);
            double term3 = Math.Pow((2.625 - x1 + x1 * Math.Pow(x2, 3)), 2);

            return (term1 + term2 + term3);
        }

        /// <summary>
        /// BRANIN Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The Branin, or Branin-Hoo, function has three global minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: x1 ∈ [-5, 10], x2 ∈ [0, 15].</param>
        /// <param name="_a">(optional) parameter. Default a = 1</param>
        /// <param name="_b">(optional) parameter. Default b = 5.1 ⁄ (4π2)</param>
        /// <param name="_c">(optional) parameter. Default c = 5 ⁄ π</param>
        /// <param name="_r">(optional) parameter. Default r = 6</param>
        /// <param name="_s">(optional) parameter. Default s = 10</param>
        /// <param name="_t">(optional) parameter. Default t = 1 ⁄ (8π)</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0.397887, at x* = (-Pi, 12.275), (Pi, 2.275) and (9.42478, 2.475).</returns> 
        public static double O_Branin(double[] x)
        {
            double? _a = new double?();
            double? _b = new double?();
            double? _c = new double?();
            double? _r = new double?();
            double? _s = new double?();
            double? _t = new double?();

            double a, b, c, r, s, t;
            if (!_a.HasValue) a = 1; else a = _a.Value;
            if (!_b.HasValue) b = (5.1 / (4 * Math.Pow(Math.PI, 2))); else b = _b.Value;
            if (!_c.HasValue) c = (5 / Math.PI); else c = _c.Value;
            if (!_r.HasValue) r = 6; else r = _r.Value;
            if (!_s.HasValue) s = 10; else s = _s.Value;
            if (!_t.HasValue) t = (1 / (8 * Math.PI)); else t = _t.Value;

            double x1 = x[0];
            double x2 = x[1];

            double term1 = a * Math.Pow((x2 - b * Math.Pow(x1, 2) + c * x1 - r), 2);
            double term2 = s * (1 - t) * Math.Cos(x1);
            return (term1 + term2 + s);
        }
        //public static double O_Branin(double[] x,
        //    double? _a = new double?(), double? _b = new double?(),
        //    double? _c = new double?(), double? _r = new double?(),
        //    double? _s = new double?(), double? _t = new double?())
        //{
        //    double a, b, c, r, s, t;
        //    if (!_a.HasValue) a = 1; else a = _a.Value;
        //    if (!_b.HasValue) b = (5.1 / (4 * Math.Pow(Math.PI, 2))); else b = _b.Value;
        //    if (!_c.HasValue) c = (5 / Math.PI); else c = _c.Value;
        //    if (!_r.HasValue) r = 6; else r = _r.Value;
        //    if (!_s.HasValue) s = 10; else s = _s.Value;
        //    if (!_t.HasValue) t = (1 / (8 * Math.PI)); else t = _t.Value;

        //    double x1 = x[0];
        //    double x2 = x[1];

        //    double term1 = a * Math.Pow((x2 - b * Math.Pow(x1, 2) + c * x1 - r), 2);
        //    double term2 = s * (1 - t) * Math.Cos(x1);
        //    return (term1 + term2 + s);
        //}


        /// <summary>
        /// BRANIN Scaled Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The Branin, or Branin-Hoo, function has three global minima.
        /// <para/> This rescaled form of the function has a mean of zero and a variance of one. The authors also add a small Gaussian error term to the output. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: x1 ∈ [-5, 10], x2 ∈ [0, 15].</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0.397887, at x* = (-Pi, 12.275), (Pi, 2.275) and (9.42478, 2.475).</returns> 
        public static double O_BraninScaled(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double x1bar = 15 * x1 - 5;
            double x2bar = 15 * x2;

            double term1 = x2bar - 5.1 * Math.Pow(x1bar, 2) / (4 * Math.Pow(Math.PI, 2)) + 5 * x1bar / Math.PI - 6;
            double term2 = (10 - 10 / (8 * Math.PI)) * Math.Cos(x1bar);

            return ((Math.Pow(term1, 2) + term2 - 44.81) / 51.95);
        }

        /// <summary>
        /// BRANIN Modified Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The Branin, or Branin-Hoo, function has three global minima.
        /// <para/> For the purpose of Kriging prediction, Forrester et al. (2008) use a modified form of the Branin-Hoo function, in which they add a term 5x1 to the response. As a result, there are two local minima and only one global minimum, making it more representative of engineering functions. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: x1 ∈ [-5, 10], x2 ∈ [0, 15].</param>
        /// <param name="_a">(optional) parameter. Default a = 1</param>
        /// <param name="_b">(optional) parameter. Default b = 5.1 ⁄ (4π2)</param>
        /// <param name="_c">(optional) parameter. Default c = 5 ⁄ π</param>
        /// <param name="_r">(optional) parameter. Default r = 6</param>
        /// <param name="_s">(optional) parameter. Default s = 10</param>
        /// <param name="_t">(optional) parameter. Default t = 1 ⁄ (8π)</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0.397887, at x* = (-Pi, 12.275), (Pi, 2.275) and (9.42478, 2.475).</returns> 
        public static double O_BraninModified(double[] x)
        {
            double? _a = new double?();
            double? _b = new double?();
            double? _c = new double?();
            double? _r = new double?();
            double? _s = new double?();
            double? _t = new double?();
            double a, b, c, r, s, t;
            if (!_a.HasValue) a = 1; else a = _a.Value;
            if (!_b.HasValue) b = (5.1 / (4 * Math.Pow(Math.PI, 2))); else b = _b.Value;
            if (!_c.HasValue) c = (5 / Math.PI); else c = _c.Value;
            if (!_r.HasValue) r = 6; else r = _r.Value;
            if (!_s.HasValue) s = 10; else s = _s.Value;
            if (!_t.HasValue) t = (1 / (8 * Math.PI)); else t = _t.Value;

            double x1 = x[0];
            double x2 = x[1];

            double term1 = a * Math.Pow((x2 - b * Math.Pow(x1, 2) + c * x1 - r), 2);
            double term2 = s * (1 - t) * Math.Cos(x1);

            return (term1 + term2 + s + 5 * x1);
        }
        //public static double O_BraninModified(double[] x,
        //    double? _a = new double?(), double? _b = new double?(),
        //    double? _c = new double?(), double? _r = new double?(),
        //    double? _s = new double?(), double? _t = new double?())
        //{
        //    double a, b, c, r, s, t;
        //    if (!_a.HasValue) a = 1; else a = _a.Value;
        //    if (!_b.HasValue) b = (5.1 / (4 * Math.Pow(Math.PI, 2))); else b = _b.Value;
        //    if (!_c.HasValue) c = (5 / Math.PI); else c = _c.Value;
        //    if (!_r.HasValue) r = 6; else r = _r.Value;
        //    if (!_s.HasValue) s = 10; else s = _s.Value;
        //    if (!_t.HasValue) t = (1 / (8 * Math.PI)); else t = _t.Value;

        //    double x1 = x[0];
        //    double x2 = x[1];

        //    double term1 = a * Math.Pow((x2 - b * Math.Pow(x1, 2) + c * x1 - r), 2);
        //    double term2 = s * (1 - t) * Math.Cos(x1);

        //    return (term1 + term2 + s + 5 * x1);
        //}



        /// <summary>
        /// COLVILLE Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 4.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-10, 10], for all i = 1, 2, 3, 4.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (1, 1, 1, 1).</returns> 
        public static double O_Colville(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            double x3 = x[2];
            double x4 = x[3];

            double term1 = 100 * Math.Pow((Math.Pow(x1, 2) - x2), 2);
            double term2 = Math.Pow(x1 - 1, 2);
            double term3 = Math.Pow(x3 - 1, 2);
            double term4 = 90 * Math.Pow(Math.Pow(x3, 2) - x4, 2);
            double term5 = 10.1 * (Math.Pow(x2 - 1, 2) + Math.Pow(x4 - 1, 2));
            double term6 = 19.8 * (x2 - 1) * (x4 - 1);

            return (term1 + term2 + term3 + term4 + term5 + term6);
        }

        /// <summary>
        /// FORRESTER Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 1.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: This function is a simple one-dimensional test function. It is multimodal, with one global minimum, one local minimum and a zero-gradient inflection point. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: x ∈ [0, 1].</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = ?, at x* = ?.</returns> 
        public static double O_Forrester(double[] x)
        {
            double fact1 = Math.Pow(6 * x[0] - 2, 2);
            double fact2 = Math.Sin(12 * x[0] - 4);
            return (fact1 * fact2);
        }

        /// <summary>
        /// FORRESTER Low Fidelity Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 1.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: This function is a simple one-dimensional test function. It is multimodal, with one global minimum, one local minimum and a zero-gradient inflection point. 
        /// <para/> Here, the constants A, B and C can be varied to improve the fidelity of the low-fidelity function.
        /// </summary>
        /// <param name="x">Decision variable. Usually: x ∈ [0, 1].</param>
        /// <param name="A">(optional) parameter. Default A = 0.5.</param>
        /// <param name="B">(optional) parameter. Default B = 10.</param>
        /// <param name="C">(optional) parameter. Default C = -5.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = ?, at x* = ?.</returns> 
        public static double O_ForresterLowFidelity(double[] x)
        {
            double A = 0.5;
            double B = 10;
            double C = -5;
            double yh = O_Forrester(x);
            double term1 = A * yh;
            double term2 = B * (x[0] - 0.5);
            return (term1 + term2 - C);
        }
        //public static double O_ForresterLowFidelity(double[] x, double A = 0.5, double B = 10, double C = -5)
        //{
        //    double yh = O_Forrester(x);
        //    double term1 = A * yh;
        //    double term2 = B * (x[0] - 0.5);
        //    return (term1 + term2 - C);
        //}

        /// <summary>
        /// GOLDSTEIN-PRICE Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.        
        /// <para/> Description: The Goldstein-Price function has several local minima. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-2, 2], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 3, at x* = (0, -1).</returns> 
        public static double O_GoldsteinPrice(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double fact1a = Math.Pow(x1 + x2 + 1, 2);
            double fact1b = 19 - 14 * x1 + 3 * Math.Pow(x1, 2) - 14 * x2 + 6 * x1 * x2 + 3 * Math.Pow(x2, 2);
            double fact1 = 1 + fact1a * fact1b;

            double fact2a = Math.Pow(2 * x1 - 3 * x2, 2);
            double fact2b = 18 - 32 * x1 + 12 * Math.Pow(x1, 2) + 48 * x2 - 36 * x1 * x2 + 27 * Math.Pow(x2, 2);
            double fact2 = 30 + fact2a * fact2b;

            return (fact1 * fact2);
        }

        /// <summary>
        /// GOLDSTEIN-PRICE Scaled Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The Goldstein-Price function has several local minima. 
        /// <para/> This rescaled logarithmic form of the function has a mean of zero and a variance of one. The authors also add a small Gaussian error term to the output.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-2, 2], for all i = 1, 2.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 3, at x* = (0, -1).</returns> 
        public static double O_GoldsteinPriceScaled(double[] x)
        {
            double x1bar = 4 * x[0] - 2;
            double x2bar = 4 * x[1] - 2;

            double fact1a = Math.Pow(x1bar + x2bar + 1, 2);
            double fact1b = 19 - 14 * x1bar + 3 * Math.Pow(x1bar, 2) - 14 * x2bar + 6 * x1bar * x2bar + 3 * Math.Pow(x2bar, 2);
            double fact1 = 1 + fact1a * fact1b;

            double fact2a = Math.Pow(2 * x1bar - 3 * x2bar, 2);
            double fact2b = 18 - 32 * x1bar + 12 * Math.Pow(x1bar, 2) + 48 * x2bar - 36 * x1bar * x2bar + 27 * Math.Pow(x2bar, 2);
            double fact2 = 30 + fact2a * fact2b;

            double prod = fact1 * fact2;

            return ((Math.Log(prod) - 8.693) / 2.427);
        }

        /// <summary>
        /// HARTMANN 3D Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 3.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The 3-dimensional Hartmann function has 4 local minima. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ (0, 1), for all i = 1, 2, 3.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -3.86278, at x* = (0.114614, 0.555649, 0.852547).</returns> 
        public static double O_Hartmann3D(double[] x)
        {
            double[] alpha = new double[] { 1, 1.2, 3, 3.2 };
            double[,] A = new double[4, 3]
                            {{3,10,30},
                            {0.1,10,35},
                            {3,10,30},
                            {0.1,10,35}};
            double[,] P = new double[4, 3] 
            { { 3689, 1170, 2673 }, 
            { 4699, 4387, 7470 }, 
            { 1091, 8732, 5547 }, 
            { 381, 5743, 8828 } };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    P[i, j] *= Math.Pow(10, -4);
                }
            }

            double outer = 0;
            for (int i = 0; i < 4; i++)
            {
                double inner = 0;
                for (int j = 0; j < 3; j++)
                {
                    double xj = x[j];
                    double Aij = A[i, j];
                    double Pij = P[i, j];
                    inner += Aij * Math.Pow(xj - Pij, 2);
                }
                double newval = alpha[i] * Math.Exp(-inner);
                outer += newval;
            }

            return -outer;
        }

        /// <summary>
        /// HARTMANN 4D Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 4.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The 4-dimensional Hartmann function is multimodal. It is given here in the form of Picheny et al. (2012), having a mean of zero and a variance of one. The authors also add a small Gaussian error term to the output.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [0, 1], for all i = 1, 2, 3, 4.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = ?, at x* = ?.</returns> 
        public static double O_Hartmann4D(double[] x)
        {
            double[] alpha = new double[] { 1, 1.2, 3, 3.2 };
            double[,] A = new double[4, 6] 
            { { 10, 3, 17, 3.5, 1.7, 8 }, 
            { 0.05, 10, 17, 0.1, 8, 14 }, 
            { 3, 3.5, 1.7, 10, 17, 8 }, 
            { 17, 8, 0.05, 10, 0.1, 14 } };

            double[,] P = new double[4, 6] 
            { { 1312, 1696, 5569, 124, 8283, 5886 }, 
            { 2329, 4135, 8307, 3736, 1004, 9991}, 
            { 2348, 1451, 3522, 2883, 3047, 6650 }, 
            { 4047, 8828, 8732, 5743, 1091, 381 } };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    P[i, j] *= Math.Pow(10, -4);
                }
            }

            double outer = 0;
            for (int i = 0; i < 4; i++)
            {
                double inner = 0;
                for (int j = 0; j < 4; j++)     //why A and P have length of 6, if this only loops till 4 ?!...
                {                               // ahh... matrices are recycled for Hartmann6D
                    double xj = x[j];
                    double Aij = A[i, j];
                    double Pij = P[i, j];
                    inner += Aij * Math.Pow(xj - Pij, 2);
                }
                double newval = alpha[i] * Math.Exp(-inner);
                outer += newval;
            }

            return ((1.1 - outer) / 0.839);
        }

        /// <summary>
        /// HARTMANN 6D Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 6.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> The 6-dimensional Hartmann function has 6 local minima. 
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ (0, 1), for all i = 1, …, 6.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -3.32237, at x* = (0.20169, 0.150011, 0.476874, 0.275332, 0.6573).</returns> 
        public static double O_Hartmann6D(double[] x)
        {
            double[] alpha = new double[] { 1, 1.2, 3, 3.2 };
            double[,] A = new double[4, 6] 
            { { 10, 3, 17, 3.5, 1.7, 8 }, 
            { 0.05, 10, 17, 0.1, 8, 14 }, 
            { 3, 3.5, 1.7, 10, 17, 8 }, 
            { 17, 8, 0.05, 10, 0.1, 14 } };

            double[,] P = new double[4, 6] 
            { { 1312, 1696, 5569, 124, 8283, 5886 }, 
            { 2329, 4135, 8307, 3736, 1004, 9991}, 
            { 2348, 1451, 3522, 2883, 3047, 6650 }, 
            { 4047, 8828, 8732, 5743, 1091, 381 } };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    P[i, j] *= Math.Pow(10, -4);
                }
            }

            double outer = 0;
            for (int i = 0; i < 4; i++)
            {
                double inner = 0;
                for (int j = 0; j < 6; j++)
                {
                    double xj = x[j];
                    double Aij = A[i, j];
                    double Pij = P[i, j];
                    inner += Aij * Math.Pow(xj - Pij, 2);
                }
                double newval = alpha[i] * Math.Exp(-inner);
                outer += newval;
            }

            return (-(2.58 + outer) / 1.94);
        }

        /// <summary>
        /// HARTMANN 6D Scaled Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 6.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> The 6-dimensional Hartmann function has 6 local minima. 
        /// <para/> This rescaled form of the function has a mean of zero and a variance of one. The authors also add a small Gaussian error term to the output.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ (0, 1), for all i = 1, …, 6.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -3.32237, at x* = (0.20169, 0.150011, 0.476874, 0.275332, 0.6573).</returns> 
        public static double O_Hartmann6DScaled(double[] x)
        {
            double[] alpha = new double[] { 1, 1.2, 3, 3.2 };
            double[,] A = new double[4, 6] 
            { { 10, 3, 17, 3.5, 1.7, 8 }, 
            { 0.05, 10, 17, 0.1, 8, 14 }, 
            { 3, 3.5, 1.7, 10, 17, 8 }, 
            { 17, 8, 0.05, 10, 0.1, 14 } };

            double[,] P = new double[4, 6] 
            { { 1312, 1696, 5569, 124, 8283, 5886 }, 
            { 2329, 4135, 8307, 3736, 1004, 9991}, 
            { 2348, 1451, 3522, 2883, 3047, 6650 }, 
            { 4047, 8828, 8732, 5743, 1091, 381 } };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    P[i, j] *= Math.Pow(10, -4);
                }
            }

            double outer = 0;
            for (int i = 0; i < 4; i++)
            {
                double inner = 0;
                for (int j = 0; j < 6; j++)
                {
                    double xj = x[j];
                    double Aij = A[i, j];
                    double Pij = P[i, j];
                    inner += Aij * Math.Pow(xj - Pij, 2);
                }
                double newval = alpha[i] * Math.Exp(-inner);
                outer += newval;
            }

            return -outer;
        }

        /// <summary>
        /// PERM D BETA Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-d, d], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (1, 2, ..., d).</returns> 
        public static double O_PermDB(double[] x)
        {
            double b = 0.5;
            int d = x.Length;

            double outer = 0;
            for (int i = 0; i < d; i++)
            {
                double inner = 0;
                for (int j = 0; j < d; j++)
                {
                    double xj = x[j];
                    inner += (Math.Pow((j + 1), (i + 1)) + b) * (Math.Pow(xj / (j + 1), (i + 1)) - 1);
                }
                outer += Math.Pow(inner, 2);
            }

            return outer;
        }
        //public static double O_PermDB(double[] x, double b = 0.5)
        //{
        //    int d = x.Length;

        //    double outer = 0;
        //    for (int i = 0; i < d; i++)
        //    {
        //        double inner = 0;
        //        for (int j = 0; j < d; j++)
        //        {
        //            double xj = x[j];
        //            inner += (Math.Pow((j + 1), (i + 1)) + b) * (Math.Pow(xj / (j + 1), (i + 1)) - 1);
        //        }
        //        outer += Math.Pow(inner, 2);
        //    }

        //    return outer;
        //}


        /// <summary>
        /// POWELL Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-4, 5], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = 0, at x* = (0, ..., 0)</returns> 
        public static double O_Powell(double[] x)
        {
            int d = x.Length;
            double sum = 0;

            for (int i = 1; i <= (d / 4); i++)
            {
                double term1 = Math.Pow(x[(4 * i - 3) - 1] + 10 * x[(4 * i - 2) - 1], 2);
                double term2 = 5 * Math.Pow(x[(4 * i - 1) - 1] - x[(4 * i) - 1], 2);
                double term3 = Math.Pow(x[(4 * i - 2) - 1] - 2 * x[(4 * i - 1) - 1], 4);
                double term4 = 10 * Math.Pow(x[(4 * i - 3) - 1] - x[(4 * i) - 1], 4);
                sum += term1 + term2 + term3 + term4;
            }
            return sum;
        }

        /// <summary>
        /// SHEKEL Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 4.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// <para/> Description: The Shekel function has m=10 local minima.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [0, 10], for all i = 1, 2, 3, 4.</param>
        /// <returns>Objective function value f(x). Global minimum at
        /// <para/>m = 5: f(x*) = -10.1532, at x* = (4, 4, 4, 4),
        /// <para/>m = 7: f(x*) = -10.4029, at x* = (4, 4, 4, 4),
        /// <para/>m = 10: f(x*) = -10.5364, at x* = (4, 4, 4, 4).</returns> 
        public static double O_Shekel(double[] x)
        {
            int m = 10;
            double[] b = new double[] { 1, 2, 2, 4, 4, 6, 3, 7, 5, 5 };
            for (int i = 0; i < b.Length; i++) b[i] *= 0.1;
            double[,] C = new double[4, 10] 
            { { 4.0, 1.0, 8.0, 6.0, 3.0, 2.0, 5.0, 8.0, 6.0, 7.0 }, 
            { 4.0, 1.0, 8.0, 6.0, 7.0, 9.0, 3.0, 1.0, 2.0, 3.0 }, 
            { 4.0, 1.0, 8.0, 6.0, 3.0, 2.0, 5.0, 8.0, 6.0, 7.0 }, 
            { 4.0, 1.0, 8.0, 6.0, 7.0, 9.0, 3.0, 1.0, 2.0, 3.0 } };

            double outer = 0;
            for (int i = 0; i < m; i++)
            {
                double bi = b[i];
                double inner = 0;
                for (int j = 0; j < 4; j++)
                {
                    double xj = x[j];
                    double Cji = C[j, i];
                    inner += Math.Pow(xj - Cji, 2);
                }
                outer += 1 / (inner + bi);
            }
            return -outer;

        }

        /// <summary>
        /// STYBLINSKI-TANG Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.        
        /// <para/> Type: Steep ridges or drops.
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [-5, 5], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum f(x*) = -39.16599d, at x* = (-2.903534, ..., -2.903534).</returns> 
        public static double O_StyblinskiTang(double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d; i++)
            {
                double xi = x[i];
                double newval = Math.Pow(xi, 4) - 16 * Math.Pow(xi, 2) + 5 * xi;
                sum += newval;
            }
            return (sum / 2);
        }

        #endregion


        #region Constrained
        /// <summary>
        /// MICHALEWICZ Function. 
        /// <para/> Objectives: 1.
        /// <para/> Real variables: 13.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 9.        
        /// <para/> Type: Constrained.
        /// <para/> Description: The objective function is quadratic and the problem has 13 variables and 9 linear constraints.
        /// <para/> Source: Michalewicz and Schoenauer 1996. Evolutionary Algorithms for Constrained Parameter Optimization Problems. In: Evolutionary Computation, 4:1, 1-32 
        /// </summary>
        /// <param name="x">Decision variable. Usually: xi ∈ [0, π], for all i = 1, …, d.</param>
        /// <returns>Objective function value f(x). Global minimum at:  
        /// <para/>f(x*) = -15, at x* = (1,1,1,1,1,1,1,1,1,3,3,3,1)</returns>
        public static double C_MichalewiczSchoenauer(double[] x)
        {
            int d = 13;
            if (d != x.Length) return double.MaxValue;
            double sum = 5 * x[0] + 5 * x[1] + 5 * x[2] + 5 * x[3];

            double firstsum = 0;
            for (int i = 0; i < 4; i++)
                firstsum += Math.Pow(x[i], 2);
            firstsum *= 5;

            double secondsum = 0;
            for (int i = 4; i < d; i++)
                secondsum += x[i];

            sum = sum - firstsum - secondsum;

            double g1, g2, g3, g4, g5, g6, g7, g8, g9;
            bool b1, b2, b3, b4, b5, b6, b7, b8, b9;
            b1 = true;
            b2 = true;
            b3 = true;
            b4 = true;
            b5 = true;
            b6 = true;
            b7 = true;
            b8 = true;
            b9 = true;
            g1 = 2 * x[0] + 2 * x[1] + x[9] + x[10];
            g2 = -8 * x[0] + x[9];
            g3 = -2 * x[3] - x[4] + x[9];
            g4 = 2 * x[0] + 2 * x[2] + x[9] + x[11];
            g5 = -8 * x[1] + x[10];
            g6 = -2 * x[5] - x[6] + x[10];
            g7 = 2 * x[1] + 2 * x[2] + x[10] + x[11];
            g8 = -8 * x[2] + x[11];
            g9 = -2 * x[7] - x[8] + x[11];
            if (g1 <= 10) b1 = false;
            if (g2 <= 0) b2 = false;
            if (g3 <= 0) b3 = false;
            if (g4 <= 10) b4 = false;
            if (g5 <= 0) b5 = false;
            if (g6 <= 0) b6 = false;
            if (g7 <= 10) b7 = false;
            if (g8 <= 0) b8 = false;
            if (g9 <= 0) b9 = false;

            double bb = Convert.ToDouble(b1) + Convert.ToDouble(b2) + Convert.ToDouble(b3) + Convert.ToDouble(b4) + Convert.ToDouble(b5) + Convert.ToDouble(b6) + Convert.ToDouble(b7) + Convert.ToDouble(b8) + Convert.ToDouble(b9);

            return sum + (bb * Math.Abs(sum));
        }

        #endregion

    }



    /// <summary>
    ///  Repository of benchmark functions for multi objective optimization.</summary>
    ///     <remarks> Types:
    ///     <para/>- MO2: 2 objectives,
    ///     <para/>- MO3: 3 obejctives,
    ///     <para/>- MOM: arbitrary number of objectives, 
    ///     <para/>- MOC: functions with constraints.
    ///     <para/>(Web-)Sources:
    ///     <para/>http://www.tik.ee.ethz.ch/sop/pisa/?page=selvar.php
    ///     <para/>https://en.wikipedia.org/wiki/Test_functions_for_optimization 
    ///     <para/>Zizler, Deb, Thiele (2000). Comparison of Multiobjective Evolutionary Algorithms: Empirical Results.
    ///     <para/>Deb, Thiele, Laumanns, Zitzler (2001). Scalable Test Problems for Evolutionary Multi-Objective Optimization.</remarks>
    public static class MO
    {

        #region 2 Objectives, no constraints
        /* 2 Objectives, NO constraints*/
        /// <summary>
        /// FONSECA-FLEMING Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: ∞.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// </summary>
        /// <param name="x">Decision variable vector. xi ∈ [-4, 4], for i = 1, …, n.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_FonsecaFleming(double[] x)
        {
            int n = x.Length;

            double sum1 = 0;
            double sum2 = 0;
            for (int i = 0; i < n; i++)
            {
                double term = x[i] - (1 / Math.Sqrt(n));
                sum1 -= Math.Pow(term, 2);
                term = x[i] + (1 / Math.Sqrt(n));
                sum2 -= Math.Pow(term, 2);
            }

            double f1 = 1 - Math.Exp(sum1);
            double f2 = 1 - Math.Exp(sum2);

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        /// <summary>
        /// KURSAWE Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: ∞ (usually 3).
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> In H. P. Schwefel and R. Männer, editors, Parallel Problem Solving from Nature. 1st Workshop, PPSN I, volume 496 of Lecture Notes in Computer Science, pages 193-197, Berlin, Germany, oct 1991. Springer-Verlag. 
        /// </summary>
        /// <param name="x">Decision variable vector. xi ∈ [-5, 5], for i = 1, …, n.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_Kursawe(double[] x)
        {
            int n = x.Length;

            double sum1 = 0;
            for (int i = 1; i <= (n - 1); i++)
            {
                sum1 -= (10 * Math.Exp(-0.2 * Math.Sqrt((Math.Pow(x[i - 1], 2) + Math.Pow(x[i], 2)))));
            }

            double sum2 = 0;
            for (int i = 1; i <= n; i++)
            {
                sum2 += (Math.Pow(Math.Abs(x[i - 1]), 0.8) + 5 * (Math.Pow(Math.Sin(x[i - 1]), 3)));
            }

            double[] fx = new double[2] { sum1, sum2 };
            return fx;
        }

        /// <summary>
        /// SCHAFFER N.1 Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: 1.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// </summary>
        /// <param name="x">Decision variable vector. x ∈ [-A, A]. Values of A form 10 to 10^{5} have been used successfully. Higher values of A increase the difficulty of the problem</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_Schaffer1(double x)
        {
            double f1 = Math.Pow(x, 2);
            double f2 = Math.Pow(x - 2, 2);

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        /// <summary>
        /// SCHAFFER N.2 Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: 1.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// </summary>
        /// <param name="x">Decision variable vector. x ∈ [-5, 10].</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_Schaffer2(double x)
        {
            double f1 = 0;
            if (x <= 1) f1 = -x;
            else if (1 < x && x <= 3) f1 = x - 2;
            else if (3 < x && x <= 4) f1 = 4 - x;
            else if (x > 4) f1 = x - 4;

            double f2 = Math.Pow(x - 5, 2);

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        /// <summary>
        /// POLONI Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// </summary>
        /// <param name="x">Decision variable vector. xi ∈ [-π, π], i = 1, 2.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_Poloni(double[] x)
        {
            double a1, a2, b1, b2;
            a1 = 0.5 * Math.Sin(1) - 2 * Math.Cos(1) + Math.Sin(2) - 1.5 * Math.Cos(2);
            a2 = 1.5 * Math.Sin(1) - Math.Cos(1) + 2 * Math.Sin(2) - 0.5 * Math.Cos(2);
            b1 = 0.5 * Math.Sin(x[0]) - 2 * Math.Cos(x[0]) + Math.Sin(x[1]) - 1.5 * Math.Cos(x[1]);
            b2 = 1.5 * Math.Sin(x[0]) - Math.Cos(x[0]) + 2 * Math.Sin(x[1]) - 0.5 * Math.Cos(x[1]);

            double f1 = 1 + Math.Pow(a1 - b1, 2) + Math.Pow(a2 - b2, 2);
            double f2 = Math.Pow(x[0] + 3, 2) + Math.Pow(x[1] + 1, 2);

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        /// <summary>
        /// ZITZLER-DEB-THIELE N.1 Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: ∞ (usually 30).
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Zizler, Deb, Thiele (2000). Comparison of Multiobjective Evolutionary Algorithms: Empirical Results.
        /// </summary>
        /// <param name="x">Decision variable vector. xi ∈ [0, 1], i = 1, ..., n.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_ZDT1(double[] x)
        {
            int m = x.Length;
            double f1 = x[0];

            double sum = 0;
            for (int i = 1; i < m; i++) { sum += x[i]; }
            double g = 1 + (9 / (m - 1)) * sum;

            double h = 1 - Math.Sqrt(f1 / g);

            double f2 = g * h;

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        /// <summary>
        /// ZITZLER-DEB-THIELE N.2 Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: ∞ (usually 30).
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Zizler, Deb, Thiele (2000). Comparison of Multiobjective Evolutionary Algorithms: Empirical Results.
        /// </summary>
        /// <param name="x">Decision variable vector. xi ∈ [0, 1], i = 1, ..., n.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_ZDT2(double[] x)
        {
            int m = x.Length;
            double f1 = x[0];

            double sum = 0;
            for (int i = 1; i < m; i++) { sum += x[i]; }
            double g = 1 + (9 / (m - 1)) * sum;

            double h = 1 - Math.Pow(f1 / g, 2);

            double f2 = g * h;

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        /// <summary>
        /// ZITZLER-DEB-THIELE N.3 Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: ∞ (usually 30).
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Zizler, Deb, Thiele (2000). Comparison of Multiobjective Evolutionary Algorithms: Empirical Results.
        /// </summary>
        /// <param name="x">Decision variable vector. xi ∈ [0, 1], i = 1, ..., n.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_ZDT3(double[] x)
        {
            int m = x.Length;
            double f1 = x[0];

            double sum = 0;
            for (int i = 1; i < m; i++) { sum += x[i]; }
            double g = 1 + (9 / (m - 1)) * sum;

            double h = 1 - Math.Sqrt(f1 / g) - (f1 / g) * Math.Sin(10 * Math.PI * f1);

            double f2 = g * h;

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        /// <summary>
        /// ZITZLER-DEB-THIELE N.4 Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: ∞ (usually 10).
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Zizler, Deb, Thiele (2000). Comparison of Multiobjective Evolutionary Algorithms: Empirical Results.
        /// </summary>
        /// <param name="x">Decision variable vector. x1 ∈ [0, 1], xi ∈ [-5, 5], i = 2, ..., n.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_ZDT4(double[] x)
        {
            int m = x.Length;
            double f1 = x[0];

            double sum = 0;
            for (int i = 1; i < m; i++) { sum += Math.Pow(x[i], 2) - 10 * Math.Cos(4 * Math.PI * x[i]); }
            double g = 1 + 10 * (m - 1) + sum;

            double h = 1 - Math.Sqrt(f1 / g);

            double f2 = g * h;

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        // ZITZLER-DEB-THIELE N.5
        //    # of real variables = 0
        //    # of bin variables = 11
        //    # of bits for binvar1 = 30
        //    # of bits for binvar2-11 = 5
        //    # of objectives = 2
        //    # of constraints = 0
        //
        //  Zizler, Deb, Thiele (2000). Comparison of Multiobjective Evolutionary Algorithms: Empirical Results
        /*
        #ifdef zdt5
        void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
        {
            int i, j;
            int u[11];
            int v[11];
            double f1, f2, g, h;
            for (i=0; i<11; i++)
            {
                u[i] = 0;
            }
            for (j=0; j<30; j++)
            {
                if (gene[0][j] == 1)
                {
                    u[0]++;
                }
            }
            for (i=1; i<11; i++)
            {
                for (j=0; j<4; j++)
                {
                    if (gene[i][j] == 1)
                    {
                        u[i]++;
                    }
                }
            }
            f1 = 1.0 + u[0];
            for (i=1; i<11; i++)
            {
                if (u[i] < 5)
                {
                    v[i] = 2 + u[i];
                }
                else
                {
                    v[i] = 1;
                }
            }
            g = 0;
            for (i=1; i<11; i++)
            {
                g += v[i];
            }
            h = 1.0/f1;
            f2 = g*h;
            obj[0] = f1;
            obj[1] = f2;
            return;
        }
        #endif*/

        /// <summary>
        /// ZITZLER-DEB-THIELE N.6 Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: ∞ (usually 10).
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Zizler, Deb, Thiele (2000). Comparison of Multiobjective Evolutionary Algorithms: Empirical Results.
        /// </summary>
        /// <param name="x">Decision variable vector. xi ∈ [0, 1], i = 1, ..., n.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO2_ZDT6(double[] x)
        {
            int m = x.Length;
            double f1 = 1 - Math.Exp(-4 * x[0]) * Math.Pow(Math.Sin(6 * Math.PI * x[0]), 6);

            double sum = 0;
            for (int i = 1; i < m; i++)
            {
                sum += x[i];
            }
            double g = 1 + 9 * Math.Pow(sum / (m - 1), 0.25);

            double h = 1 - Math.Pow(f1 / g, 2);

            double f2 = g * h;

            if (double.IsNaN(f2)) f2 = double.MaxValue;
            if (double.IsNaN(f1)) f1 = double.MaxValue;

            double[] fx = new double[2] { f1, f2 };
            return fx;
        }

        #endregion





        #region 3 Objectives, no constraints
        /* 3 Objectives, NO constraints */

        /// <summary>
        /// VIENNET Function. 
        /// <para/> Objectives: 3.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// </summary>
        /// <param name="x">Decision variable vector. xi ∈ [-3, 3], i = 1, 2.</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MO3_Viennet(double[] x)
        {
            double f1 = 0.5 * (Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) + Math.Sin(Math.Pow(x[0], 2) + Math.Pow(x[1], 2));
            double f2 = (Math.Pow(3 * x[0] - 2 * x[1] + 4, 2) / 8) + (Math.Pow(x[0] - x[1] + 1, 2) / 27) + 15;
            double f3 = (1 / (Math.Pow(x[0], 2) + Math.Pow(x[1], 2) + 1)) - 1.1 * Math.Exp(-(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)));

            double[] fx = new double[3] { f1, f2, f3 };
            return fx;
        }

        #endregion





        #region Many Objectives, no constraints
        /* Many Objectives, NO constraints*/
        /// <summary>
        /// DEB-THIELE-LAUMANNS-ZITZLER N.1 Function. 
        /// <para/> Objectives: M ϵ [2,10].
        /// <para/> Real variables: ∞. k = 5 suggested. (k = n - M + 1), where n is number of variables.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Deb, Thiele, Laumanns, Zitzler (2001). Scalable Test Problems for Evolutionary Multi-Objective Optimization.
        /// </summary>
        /// <param name="x">Decision variable vector. xi, i = 1, ..., n.</param>
        /// <param name="M">Number of Objectives. M ϵ [2,10].</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MOM_DTLZ1(double[] x, int M)
        {
            int i = 0, j = 0;
            int n = x.Length;   // length of decision variables
            int k = n - M + 1;  // M is number of objectives

            double g = 0;
            for (i = n - k + 1; i <= n; i++)
            {
                g += Math.Pow(x[i - 1] - 0.5, 2) - Math.Cos(20 * Math.PI * (x[i - 1] - 0.5));
            }
            g = 100 * (k + g);

            double[] fx = new double[M];
            for (i = 1; i <= M; i++)
            {
                double f = 0.5 * (1 + g);
                for (j = M - i; j >= 1; j--)
                {
                    f *= x[j - 1];
                }
                if (i > 1)
                {
                    f *= 1 - x[(M - i + 1) - 1];
                }

                fx[i - 1] = f;
            }

            return fx;
        }

        /// <summary>
        /// DEB-THIELE-LAUMANNS-ZITZLER N.2 Function. 
        /// <para/> Objectives: M ϵ [2,10].
        /// <para/> Real variables: ∞. k = 10 suggested. (k = n - M + 1), where n is number of variables.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Deb, Thiele, Laumanns, Zitzler (2001). Scalable Test Problems for Evolutionary Multi-Objective Optimization.
        /// </summary>
        /// <param name="x">Decision variable vector. xi, i = 1, ..., n.</param>
        /// <param name="M">Number of Objectives. M ϵ [2,10].</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MOM_DTLZ2(double[] x, int M)
        {
            int i = 0, j = 0;
            int n = x.Length;
            int k = n - M + 1;

            double g = 0;
            for (i = n - k + 1; i <= n; i++)
            {
                g += Math.Pow(x[i - 1] - 0.5, 2);
            }

            double[] fx = new double[M];
            for (i = 1; i <= M; i++)
            {
                double f = (1 + g);
                for (j = M - i; j >= 1; j--)
                {
                    f *= Math.Cos(x[j - 1] * Math.PI / 2);
                }
                if (i > 1)
                {
                    f *= Math.Sin(x[(M - i + 1) - 1] * Math.PI / 2);
                }

                fx[i - 1] = f;
            }

            return fx;
        }

        /// <summary>
        /// DEB-THIELE-LAUMANNS-ZITZLER N.3 Function. 
        /// <para/> Objectives: M ϵ [2,10].
        /// <para/> Real variables: ∞. k = 10 suggested. (k = n - M + 1), where n is number of variables.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Deb, Thiele, Laumanns, Zitzler (2001). Scalable Test Problems for Evolutionary Multi-Objective Optimization.
        /// </summary>
        /// <param name="x">Decision variable vector. xi, i = 1, ..., n.</param>
        /// <param name="M">Number of Objectives. M ϵ [2,10].</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MOM_DTLZ3(double[] x, int M)
        {
            int i = 0, j = 0;
            int n = x.Length;
            int k = n - M + 1;

            double g = 0;
            for (i = n - k + 1; i <= n; i++)
            {
                g += Math.Pow(x[i - 1] - 0.5, 2) - Math.Cos(20.0 * Math.PI * (x[i - 1] - 0.5));
            }
            g = 100 * (k + g);

            double[] fx = new double[M];
            for (i = 1; i <= M; i++)
            {
                double f = (1 + g);
                for (j = M - i; j >= 1; j--)
                {
                    f *= Math.Cos(x[j - 1] * Math.PI / 2.0);
                }
                if (i > 1)
                {
                    f *= Math.Sin(x[(M - i + 1) - 1] * Math.PI / 2.0);
                }

                fx[i - 1] = f;
            }

            return fx;
        }

        /// <summary>
        /// DEB-THIELE-LAUMANNS-ZITZLER N.4 Function. 
        /// <para/> Objectives: M ϵ [2,10].
        /// <para/> Real variables: ∞. k = 10 suggested. (k = n - M + 1), where n is number of variables.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Deb, Thiele, Laumanns, Zitzler (2001). Scalable Test Problems for Evolutionary Multi-Objective Optimization.
        /// </summary>
        /// <param name="x">Decision variable vector. xi, i = 1, ..., n.</param>
        /// <param name="M">Number of Objectives. M ϵ [2,10].</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MOM_DTLZ4(double[] x, int M, double alpha = 100)
        {
            int i = 0, j = 0;
            int n = x.Length;
            int k = n - M + 1;

            double g = 0;
            for (i = n - k + 1; i <= n; i++)
            {
                g += Math.Pow(x[i - 1] - 0.5, 2);
            }

            double[] fx = new double[M];
            for (i = 1; i <= M; i++)
            {
                double f = (1 + g);
                for (j = M - i; j >= 1; j--)
                {
                    f *= Math.Cos(Math.Pow(x[j - 1], alpha) * Math.PI / 2);
                }
                if (i > 1)
                {
                    f *= Math.Sin(Math.Pow(x[(M - i + 1) - 1], alpha) * Math.PI / 2);
                }

                fx[i - 1] = f;
            }

            return fx;
        }

        /// <summary>
        /// DEB-THIELE-LAUMANNS-ZITZLER N.5 Function. 
        /// <para/> Objectives: M ϵ [2,10].
        /// <para/> Real variables: ∞. k = 10 suggested. (k = n - M + 1), where n is number of variables.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Deb, Thiele, Laumanns, Zitzler (2001). Scalable Test Problems for Evolutionary Multi-Objective Optimization.
        /// </summary>
        /// <param name="x">Decision variable vector. xi, i = 1, ..., n.</param>
        /// <param name="M">Number of Objectives. M ϵ [2,10].</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MOM_DTLZ5(double[] x, int M)
        {
            int i = 0, j = 0;
            int n = x.Length;
            int k = n - M + 1;
            double[] theta = new double[M];
            double t = 0;
            double g = 0;

            for (i = n - k + 1; i <= n; i++)
            {
                g += Math.Pow(x[i - 1] - 0.5, 2);
            }

            t = Math.PI / (4 * (1 + g));
            theta[0] = x[0] * Math.PI / 2;
            for (i = 2; i <= M - 1; i++)
            {
                theta[i - 1] = t * (1 + 2 * g * x[i - 1]);
            }

            double[] fx = new double[M];
            for (i = 1; i <= M; i++)
            {
                double f = (1 + g);
                for (j = M - i; j >= 1; j--)
                {
                    f *= Math.Cos(theta[j - 1]);
                }
                if (i > 1)
                {
                    f *= Math.Sin(theta[(M - i + 1) - 1]);
                }

                fx[i - 1] = f;
            }

            return fx;
        }

        /// <summary>
        /// DEB-THIELE-LAUMANNS-ZITZLER N.6 Function. 
        /// <para/> Objectives: M ϵ [2,10].
        /// <para/> Real variables: ∞. k = 10 suggested. (k = n - M + 1), where n is number of variables.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Deb, Thiele, Laumanns, Zitzler (2001). Scalable Test Problems for Evolutionary Multi-Objective Optimization.
        /// </summary>
        /// <param name="x">Decision variable vector. xi, i = 1, ..., n.</param>
        /// <param name="M">Number of Objectives. M ϵ [2,10].</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MOM_DTLZ6(double[] x, int M)
        {
            int i = 0, j = 0;
            int n = x.Length;
            int k = n - M + 1;
            double[] theta = new double[M];
            double t = 0;
            double g = 0;

            for (i = n - k + 1; i <= n; i++)
            {
                g += Math.Pow(x[i - 1], 0.1);
            }

            t = Math.PI / (4 * (1 + g));
            theta[0] = x[0] * Math.PI / 2;
            for (i = 2; i <= M - 1; i++)
            {
                theta[i - 1] = t * (1 + 2 * g * x[i - 1]);
            }

            double[] fx = new double[M];
            for (i = 1; i <= M; i++)
            {
                double f = (1 + g);
                for (j = M - i; j >= 1; j--)
                {
                    f *= Math.Cos(theta[j - 1]);
                }
                if (i > 1)
                {
                    f *= Math.Sin(theta[(M - i + 1) - 1]);
                }

                fx[i - 1] = f;
            }

            return fx;
        }

        /// <summary>
        /// DEB-THIELE-LAUMANNS-ZITZLER N.7 Function. 
        /// <para/> Objectives: M ϵ [2,10].
        /// <para/> Real variables: ∞. k = 20 suggested. (k = n - M + 1), where n is number of variables.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 0.
        /// <para/> Source: Deb, Thiele, Laumanns, Zitzler (2001). Scalable Test Problems for Evolutionary Multi-Objective Optimization.
        /// </summary>
        /// <param name="x">Decision variable vector. xi, i = 1, ..., n.</param>
        /// <param name="M">Number of Objectives. M ϵ [2,10].</param>
        /// <returns>Objective function values f(x).</returns>
        public static double[] MOM_DTLZ7(double[] x, int M)
        {
            int i = 0, j = 0;
            int n = x.Length;
            int k = n - M + 1;
            double g = 0;
            double h = 0;

            for (i = n - k + 1; i <= n; i++)
            {
                g += x[i - 1];
            }
            g = 1 + 9 * g / k;

            double[] fx = new double[M];
            for (i = 1; i <= M - 1; i++)
            {
                fx[i - 1] = x[i - 1];
            }

            for (j = 1; j <= M - 1; j++)
            {
                h += x[j - 1] / (1 + g) * (1 + Math.Sin(3 * Math.PI * x[j - 1]));
            }
            h = M - h;
            fx[M - 1] = (1 + g) * h;

            return fx;
        }

        #endregion




        #region With Constrainst
        /* WITH CONSTRAINTS 
        negative value means constraint is violated.
        0 is ok, if <= or >=
        if only < or >, then 0 is also indicating violation*/

        /// <summary>
        /// BINH-KORN Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 2.
        /// </summary>
        /// <param name="x">Decision variable vector. x1 ϵ [0, 5], x2 ϵ [0, 3].</param>
        /// <returns>[0][]: Objective function values f(x).
        /// <para/>[1][]: Constraints violation. Negative value means constraint is violated. 0-value accepted, if smaller/greater-equals constraint, otherwise 0 also indicates violation.</returns>
        public static double[][] MO2C_BinhKorn(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];

            double f1 = 4 * Math.Pow(x1, 2) + 4 * Math.Pow(x2, 2);
            double f2 = Math.Pow(x1 - 5, 2) + Math.Pow(x2 - 5, 2);

            double g1 = 1 - (Math.Pow(x1 - 5, 2) + Math.Pow(x2 - 5, 2)) / 25;  // <=25 
            double g2 = (Math.Pow(x1 - 8, 2) + Math.Pow(x2 + 3, 2)) / 7.7 - 1; // >= 7.7

            double[] fx = new double[2] { f1, f2 };
            double[] g = new double[2] { g1, g2 };
            double[][] result = new double[2][];
            result[0] = fx;
            result[1] = g;
            return result;
        }

        /// <summary>
        /// OSYCZKA-KUNDU Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: 6.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 6.
        /// </summary>
        /// <param name="x">Decision variable vector. x1, x2, x6 ϵ [0, 10], x3, x5 ϵ [1, 5], x4 ϵ [0, 6].</param>
        /// <returns>[0][]: Objective function values f(x).
        /// <para/>[1][]: Constraints violation. Negative value means constraint is violated. 0-value accepted, if smaller/greater-equals constraint, otherwise 0 also indicates violation.</returns>
        public static double[][] MO2C_OsyczkaKundu(double[] x)
        {
            double f1 = -(25 * Math.Pow(x[0] - 2, 2) + Math.Pow(x[1] - 2, 2) + Math.Pow(x[2] - 1, 2) + Math.Pow(x[3] - 4, 2) + Math.Pow(x[4] - 1, 2));
            double f2 = 0;
            for (int i = 0; i < 6; i++) { f2 += Math.Pow(x[i], 2); }

            double[] g = new double[6];
            g[0] = (x[0] + x[1]) / 2 - 1;                       // >=2
            g[1] = 1 - (x[0] - x[1]) / 6;                       // <=6
            g[2] = 1 - x[1] / 2 + x[0] / 2;                     // <=2
            g[3] = 1 - x[0] / 2 + 3 * x[1] / 2;                 // <= 2
            g[4] = 1 - Math.Pow(x[2] - 3, 2) / 4 - x[3] / 4;    // <= 4
            g[5] = Math.Pow(x[4] - 3, 2) / 4 + x[5] / 4 - 1;    // >= 4

            double[] fx = new double[2] { f1, f2 };
            double[][] result = new double[2][];
            result[0] = fx;
            result[1] = g;
            return result;
        }

        /// <summary>
        /// CHAKONG-HAIMES Function. 
        /// <para/> Objectives: 2.
        /// <para/> Real variables: 2.
        /// <para/> Binary variables: 0.
        /// <para/> Constraints: 2.
        /// </summary>
        /// <param name="x">Decision variable vector. x1, x2 ϵ [-20, 20].</param>
        /// <returns>[0][]: Objective function values f(x).
        /// <para/>[1][]: Constraints violation. Negative value means constraint is violated. 0-value accepted, if smaller/greater-equals constraint, otherwise 0 also indicates violation.</returns>
        public static double[][] MO2C_ChakongHaimes(double[] x)
        {
            double f1 = 2 + Math.Pow(x[0] - 2, 2) + Math.Pow(x[1] - 1, 2);
            double f2 = 9 * x[0] - Math.Pow(x[1] - 1, 2);
            double g1 = 1 - (Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) / 225;
            double g2 = 3 * x[1] / 10 - x[0] / 10 - 1;

            double[][] result = new double[2][];
            result[0] = new double[] { f1, f2 };
            result[1] = new double[] { g1, g2 };
            return result;
        }

        #endregion
    }
}


#region ToDoDebLab
// from http://www.iitk.ac.in/kangal/codes.shtml

//*  Test problem TNK
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 2
//    */

//#ifdef tnk
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    obj[0] = xreal[0];
//    obj[1] = xreal[1];
//    if (xreal[1] == 0.0)
//    {
//        constr[0] = -1.0;
//    }
//    else
//    {
//        constr[0] = xreal[0]*xreal[0] + xreal[1]*xreal[1] - 0.1*cos(16.0*atan(xreal[0]/xreal[1])) - 1.0;
//    }
//    constr[1] = 1.0 - 2.0*pow((xreal[0]-0.5),2.0) + 2.0*pow((xreal[1]-0.5),2.0);
//    return;
//}
//#endif

///*  Test problem CTP1
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 2
//    */

//#ifdef ctp1
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    double g;
//    g = 1.0 + xreal[1];
//    obj[0] = xreal[0];
//    obj[1] = g*exp(-obj[0]/g);
//    constr[0] = obj[1]/(0.858*exp(-0.541*obj[0]))-1.0;
//    constr[1] = obj[1]/(0.728*exp(-0.295*obj[0]))-1.0;
//    return;
//}
//#endif

///*  Test problem CTP2
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 1
//    */

//#ifdef ctp2
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    double g;
//    double theta, a, b, c, d, e;
//    double exp1, exp2;
//    theta = -0.2*PI;
//    a = 0.2;
//    b = 10.0;
//    c = 1.0;
//    d = 6.0;
//    e = 1.0;
//    g = 1.0 + xreal[1];
//    obj[0] = xreal[0];
//    obj[1] = g*(1.0  - sqrt(obj[0]/g));
//    exp1 = (obj[1]-e)*cos(theta) - obj[0]*sin(theta);
//    exp2 = (obj[1]-e)*sin(theta) + obj[0]*cos(theta);
//    exp2 = b*PI*pow(exp2,c);
//    exp2 = fabs(sin(exp2));
//    exp2 = a*pow(exp2,d);
//    constr[0] = exp1/exp2 - 1.0;
//    return;
//}
//#endif

///*  Test problem CTP3
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 1
//    */

//#ifdef ctp3
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    double g;
//    double theta, a, b, c, d, e;
//    double exp1, exp2;
//    theta = -0.2*PI;
//    a = 0.1;
//    b = 10.0;
//    c = 1.0;
//    d = 0.5;
//    e = 1.0;
//    g = 1.0 + xreal[1];
//    obj[0] = xreal[0];
//    obj[1] = g*(1.0  - sqrt(obj[0]/g));
//    exp1 = (obj[1]-e)*cos(theta) - obj[0]*sin(theta);
//    exp2 = (obj[1]-e)*sin(theta) + obj[0]*cos(theta);
//    exp2 = b*PI*pow(exp2,c);
//    exp2 = fabs(sin(exp2));
//    exp2 = a*pow(exp2,d);
//    constr[0] = exp1/exp2 - 1.0;
//    return;
//}
//#endif

///*  Test problem CTP4
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 1
//    */

//#ifdef ctp4
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    double g;
//    double theta, a, b, c, d, e;
//    double exp1, exp2;
//    theta = -0.2*PI;
//    a = 0.75;
//    b = 10.0;
//    c = 1.0;
//    d = 0.5;
//    e = 1.0;
//    g = 1.0 + xreal[1];
//    obj[0] = xreal[0];
//    obj[1] = g*(1.0  - sqrt(obj[0]/g));
//    exp1 = (obj[1]-e)*cos(theta) - obj[0]*sin(theta);
//    exp2 = (obj[1]-e)*sin(theta) + obj[0]*cos(theta);
//    exp2 = b*PI*pow(exp2,c);
//    exp2 = fabs(sin(exp2));
//    exp2 = a*pow(exp2,d);
//    constr[0] = exp1/exp2 - 1.0;
//    return;
//}
//#endif

///*  Test problem CTP5
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 1
//    */

//#ifdef ctp5
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    double g;
//    double theta, a, b, c, d, e;
//    double exp1, exp2;
//    theta = -0.2*PI;
//    a = 0.1;
//    b = 10.0;
//    c = 2.0;
//    d = 0.5;
//    e = 1.0;
//    g = 1.0 + xreal[1];
//    obj[0] = xreal[0];
//    obj[1] = g*(1.0  - sqrt(obj[0]/g));
//    exp1 = (obj[1]-e)*cos(theta) - obj[0]*sin(theta);
//    exp2 = (obj[1]-e)*sin(theta) + obj[0]*cos(theta);
//    exp2 = b*PI*pow(exp2,c);
//    exp2 = fabs(sin(exp2));
//    exp2 = a*pow(exp2,d);
//    constr[0] = exp1/exp2 - 1.0;
//    return;
//}
//#endif

///*  Test problem CTP6
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 1
//    */

//#ifdef ctp6
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    double g;
//    double theta, a, b, c, d, e;
//    double exp1, exp2;
//    theta = 0.1*PI;
//    a = 40.0;
//    b = 0.5;
//    c = 1.0;
//    d = 2.0;
//    e = -2.0;
//    g = 1.0 + xreal[1];
//    obj[0] = xreal[0];
//    obj[1] = g*(1.0  - sqrt(obj[0]/g));
//    exp1 = (obj[1]-e)*cos(theta) - obj[0]*sin(theta);
//    exp2 = (obj[1]-e)*sin(theta) + obj[0]*cos(theta);
//    exp2 = b*PI*pow(exp2,c);
//    exp2 = fabs(sin(exp2));
//    exp2 = a*pow(exp2,d);
//    constr[0] = exp1/exp2 - 1.0;
//    return;
//}
//#endif

///*  Test problem CTP7
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 1
//    */

//#ifdef ctp7
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    double g;
//    double theta, a, b, c, d, e;
//    double exp1, exp2;
//    theta = -0.05*PI;
//    a = 40.0;
//    b = 5.0;
//    c = 1.0;
//    d = 6.0;
//    e = 0.0;
//    g = 1.0 + xreal[1];
//    obj[0] = xreal[0];
//    obj[1] = g*(1.0  - sqrt(obj[0]/g));
//    exp1 = (obj[1]-e)*cos(theta) - obj[0]*sin(theta);
//    exp2 = (obj[1]-e)*sin(theta) + obj[0]*cos(theta);
//    exp2 = b*PI*pow(exp2,c);
//    exp2 = fabs(sin(exp2));
//    exp2 = a*pow(exp2,d);
//    constr[0] = exp1/exp2 - 1.0;
//    return;
//}
//#endif

///*  Test problem CTP8
//    # of real variables = 2
//    # of bin variables = 0
//    # of objectives = 2
//    # of constraints = 2
//    */

//#ifdef ctp8
//void test_problem (double *xreal, double *xbin, int **gene, double *obj, double *constr)
//{
//    double g;
//    double theta, a, b, c, d, e;
//    double exp1, exp2;
//    g = 1.0 + xreal[1];
//    obj[0] = xreal[0];
//    obj[1] = g*(1.0  - sqrt(obj[0]/g));
//    theta = 0.1*PI;
//    a = 40.0;
//    b = 0.5;
//    c = 1.0;
//    d = 2.0;
//    e = -2.0;
//    exp1 = (obj[1]-e)*cos(theta) - obj[0]*sin(theta);
//    exp2 = (obj[1]-e)*sin(theta) + obj[0]*cos(theta);
//    exp2 = b*PI*pow(exp2,c);
//    exp2 = fabs(sin(exp2));
//    exp2 = a*pow(exp2,d);
//    constr[0] = exp1/exp2 - 1.0;
//    theta = -0.05*PI;
//    a = 40.0;
//    b = 2.0;
//    c = 1.0;
//    d = 6.0;
//    e = 0.0;
//    exp1 = (obj[1]-e)*cos(theta) - obj[0]*sin(theta);
//    exp2 = (obj[1]-e)*sin(theta) + obj[0]*cos(theta);
//    exp2 = b*PI*pow(exp2,c);
//    exp2 = fabs(sin(exp2));
//    exp2 = a*pow(exp2,d);
//    constr[1] = exp1/exp2 - 1.0;
//    return;
//}
//#endif
#endregion

#region ToDoPISA


//int eval_COMET(individual *ind)
//{
//    double x1;
//    double x2;
//    double x3;
//    double g;

//    assert(number_decision_variables == 3);
//    assert(dimension == 3);

//    x1 = 1 + (ind->x[0] * 2.5);
//    x2 = -2 + (ind->x[1] * 4);
//    x3 = ind->x[2];

//    g = x3;

//    ind->f[0] = (1 + g) * (pow(x1,3) * pow(x2,2) - 10 * x1 - 4 * x2);
//    ind->f[1] = (1 + g) * (pow(x1,3) * pow(x2,2) - 10 * x1 + 4 * x2);
//    ind->f[2] = 3 * (1 + g) * pow(x1,2);

//    ind->f[0] = ind->f[0] + 100;
//    ind->f[1] = ind->f[1] + 100;
//    ind->f[2] = ind->f[2];


//    return(0);
//}

//int eval_ZDT1(individual *ind)
//{    
//    int i = 0;
//    int n = number_decision_variables;
//    double f1 = 0;
//    double g = 0;
//    double h = 0;

//    assert(dimension == 2);
//    assert(number_decision_variables >= 2);

//    f1 = ind->x[0];

//    for (i = 1; i < n; i++)
//    {
//    g += ind->x[i];
//    }
//    g = 1 + 9 * g / (n-1);
//    h = 1 - sqrt(f1 / g);

//    ind->f[0] = f1;
//    ind->f[1] = g * h;

//    return(0);
//}

//int eval_ZDT2(individual *ind)
//{    
//    int i = 0;
//    int n = number_decision_variables;
//    double f1 = 0;
//    double g = 0;
//    double h = 0;

//    assert(dimension == 2);
//    assert(number_decision_variables >= 2);

//    f1 = ind->x[0];

//    for (i = 1; i < n; i++)
//    {
//    g += ind->x[i];
//    }
//    g = 1 + 9 * g / (n-1);
//    h = 1 - pow(f1 / g, 2);

//    ind->f[0] = f1;
//    ind->f[1] = g * h;

//    return(0);
//}

//int eval_ZDT3(individual *ind)
//{    
//    int i = 0;
//    int n = number_decision_variables;
//    double f1 = 0;
//    double g = 0;
//    double h = 0;

//    assert(dimension == 2);
//    assert(number_decision_variables >= 2);

//    f1 = ind->x[0];

//    for (i = 1; i < n; i++)
//    {
//    g += ind->x[i];
//    }
//    g = 1 + 9 * g / (n-1);
//    h = 1 - sqrt(f1 / g) - (f1 / g) * sin(10 * PISA_PI * f1);

//    ind->f[0] = f1;
//    ind->f[1] = g * h + 1;

//    return(0);
//}

//int eval_ZDT4(individual *ind)
//{    
//    int i = 0;
//    int n = number_decision_variables;
//    double f1 = 0;
//    double g = 0;
//    double h = 0;

//    assert(dimension == 2);
//    assert(number_decision_variables >= 2);

//    f1 = ind->x[0];

//    for (i = 1; i < n; i++)
//    {
//    double x = ind->x[i];
//    g += x * x - 10 * cos(4 * PISA_PI * x);
//    }
//    g = 1 + 10 * (n - 1) + g;
//    h = 1 - sqrt(f1 / g);

//    ind->f[0] = f1;
//    ind->f[1] = g * h;

//    return(0);
//}

//int eval_ZDT6(individual *ind)
//{    
//    int i = 0;
//    int n = number_decision_variables;
//    double f1 = 0;
//    double g = 0;
//    double h = 0;

//    assert(dimension == 2);
//    assert(number_decision_variables >= 2);

//    f1 = 1 - exp(-4 * ind->x[0]) * pow(sin(6 * PISA_PI * ind->x[0]), 6);

//    for (i = 1; i < n; i++)
//    {
//    g += ind->x[i];
//    }
//    g = 1 + 9 * pow(g / (n-1), 0.25);
//    h = 1 - pow(f1 / g, 2);

//    ind->f[0] = f1;
//    ind->f[1] = g * h;

//    return(0);
//}

//int eval_SPHERE(individual *ind)
//{    
//    int i, j;
//    int n = number_decision_variables;
//    int m = dimension;

//    for (j = 0; j < m; j++)
//    {
//        double f = 0.0;
//        for (i = 0; i < n; i++)
//        {
//            double x = -1000 + 2000 * ind->x[(i + j) % n];
//            if (i == 0)
//        {
//                x = x - 1;
//        }
//            f += x * x;
//        }
//        ind->f[j] = f;
//    }

//    return(0);
//}

//int eval_KUR(individual *ind)
//{    
//    int i;
//    int n = number_decision_variables;
//    double f = 0;

//    assert(dimension == 2);

//    for (i = 0; i < n; i++)
//    {
//    double x = -10 + 20 * ind->x[i];
//        f += pow(fabs(x), 0.8) + 5 * pow(sin(x), 3) + 3.5828;
//    }

//    ind->f[0] = f;

//    f = 0;
//    for (i = 0; i < n-1; i++)
//    {
//    double x = -10 + 20 * ind->x[i];
//    double x1 = -10 + 20 * ind->x[i+1];
//    f += 1 - exp(-0.2 * sqrt(pow(x, 2) + pow(x1, 2)));
//    }

//    ind->f[1] = f;

//    return(0);
//}

//int eval_QV(individual *ind)
//{    
//    int i;
//    int n = number_decision_variables;
//    double F1 = 0;
//    double F2 = 0;

//    assert(dimension == 2);

//    for (i = 0; i < n; i++)
//    {
//        double x = -5 + 10 * ind->x[i];
//        F1 += (x)*(x) - 10*cos(2*PISA_PI*(x)) + 10;
//        F2 += (x-1.5)*(x-1.5) - 10*cos(2*PISA_PI*(x-1.5)) + 10;
//    }
//    F1 = pow((F1/n),0.25);
//    F2 = pow((F2/n),0.25);

//    ind->f[0] = F1;
//    ind->f[1] = F2;

//    return(0);
//}


#endregion