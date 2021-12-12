using System;
using System.Collections.Generic;
using System.Text;

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

        public double[,] CountProb_MC_Conditional(double[] prob_C, double[,] prob_MC)
        {
            double[,] prob_MC_Cond = new double[prob_C.Length, prob_C.Length];

            for (int i = 0; i < prob_C.Length; i++)
            {
                for (int j = 0; j < prob_C.Length; j++)
                {
                    prob_MC_Cond[i,j] = prob_MC[i,j]/prob_C[i];
                }
            }

            return prob_MC_Cond;
        }

    }
}
