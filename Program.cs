using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static double[] K_array = new double[20];
        static double[] M_array = new double[20];
        static int[,] C = new int[20, 20];

        static string fileTable = "C:/table_06.csv";
        static string fileProb = "C:/prob_06.csv";

        static double[] prob_C = new double[20];
        static double[,] prob_MC = new double[20, 20];
        //static double[,] prob_MC_cond = new double[20, 20];

        static void Main(string[] args)
        {
            var fileHelper = new Files();
            var statCounts = new Counts();

            var C_double = fileHelper.ReadTable(fileTable, 20, 20);
            C = fileHelper.Intify(C_double);

            var M_K = fileHelper.ReadTable(fileProb, 2, 20);
            M_array = fileHelper.ToLine(M_K, 0);
            K_array = fileHelper.ToLine(M_K, 1);

            prob_C = statCounts.CountProb_C(M_array, K_array, C);
            prob_MC = statCounts.CountProb_MC(M_array, K_array, C);
            var  prob_MC_cond = statCounts.CountProb_MC_Conditional(prob_C, prob_MC);
            var x = statCounts.Determinictic(prob_MC_cond);

            var mmm = statCounts.AveLossDet(x, prob_MC_cond, prob_C);
            var hren = statCounts.Stochastic(prob_MC_cond);

            var derimo = statCounts.AveLossStoch(hren, prob_MC_cond, prob_C);
            Console.WriteLine(derimo);
            Console.WriteLine(mmm);
            Console.ReadKey();
        }
    }
}
