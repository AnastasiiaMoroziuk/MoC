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

        static string fileTable = "D:/Programming/MoC/vars/vars/table_06.csv";
        static string fileProb = "D:/Programming/MoC/vars/vars/prob_06.csv";

        static double[] prob_C = new double[20];
        static double[,] prob_MC = new double[20, 20];

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

            var deterministic = statCounts.Determinictic(prob_MC_cond);
            var aveLossDat = statCounts.AveLossDet(deterministic, prob_MC_cond, prob_C);

            var stochastic = statCounts.Stochastic(prob_MC_cond);
            var aveLossStoch = statCounts.AveLossStoch(stochastic, prob_MC_cond, prob_C);



            Console.WriteLine("Условное распределение:");
            fileHelper.DisplayList(prob_MC_cond);
            Console.WriteLine("\n\n");
            //----------------------------------------------------------------------------------------------------
            Console.WriteLine("Детерминистическая решающая функция:");
            fileHelper.DisplayArr(deterministic);
            Console.WriteLine("");
            Console.WriteLine("Средние потери для детерминистической решающей функции:");
            Console.WriteLine(aveLossDat);
            Console.WriteLine("\n\n");
            //----------------------------------------------------------------------------------------------------

            Console.WriteLine("Стохастическая решающая функция:");
            fileHelper.DisplayList(stochastic);

            Console.WriteLine("Средние потери для стохастической решающей функции:");
            Console.WriteLine(aveLossStoch);
            Console.WriteLine("\n\n");

            Console.ReadKey();
        }
    }
}
