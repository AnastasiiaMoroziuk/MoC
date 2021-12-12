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

    }
}
