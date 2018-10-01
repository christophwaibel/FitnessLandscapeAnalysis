using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessLandscapeAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Sequences.parameters param = new Sequences.parameters();
            param.distribution = "normal";
            param.seed = 3;
            param.stepsize = 0.05;
            

            double[][] x = Sequences.RandomWalk(2,100,param);

            Console.ReadKey();
        }
    }
}
