using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp2
{
    class Counts
    {
        public double[] CountProb_C(double[] M, double[] K, int[,] C)
        {

            var prob_C = new double[C.GetLength(1)];

            for (int i = 0; i < C.GetLength(0); i++)
            {
                for (int j = 0; j < C.GetLength(1); j++)
                {
                    prob_C[C[i, j]] += K[i] * M[j];
                }
            }

            return prob_C;
        }



        public double[,] CountProb_MC(double[] M, double[] K, int[,] C)
        {
            var prob_MC = new double[C.GetLength(0), C.GetLength(1)];
            for (int i = 0; i < prob_MC.GetLength(0); i++)
            {
                for (int j = 0; j < prob_MC.GetLength(1); j++)
                {
                    prob_MC[C[i, j], j] += K[i] * M[j];
                }
            }

            return prob_MC;
        }

        public List<List<double>> CountProb_MC_Conditional(double[] prob_C, double[,] prob_MC)
        {
            var prob_MC_Cond = new List<List<double>>();

            for (int i = 0; i < prob_C.Length; i++)
            {
                prob_MC_Cond.Add(new List<double>());
                for (int j = 0; j < prob_C.Length; j++)
                {
                    prob_MC_Cond[i].Add(prob_MC[i,j]/prob_C[i]);
                }
            }

            return prob_MC_Cond;
        }

        public List<double> Determinictic(List<List<double>> prob_MC_Cond) =>
            prob_MC_Cond.Select(el => (double)el.IndexOf(el.Max())).ToList();

        public double AveLossDet(double[] D, double[,] prob_MC_Cond, double[] prob_C) =>
            prob_C
            .Select((el, i) => el * (1 - prob_MC_Cond[i, (int)D[i]]))
            .ToArray()
            .Sum();
    }
}
