﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp2
{
    class Counts
    {
        public double[] CountProb_C(double[] M, double[] K, int[,] C)
        {

            var prob_C = new double[M.Length];

            for (int i = 0; i < M.Length; i++)
            {
                for (int j = 0; j < M.Length; j++)
                {
                    prob_C[C[i, j]] += K[i] * M[j];
                }
            }

            return prob_C;
        }



        public double[,] CountProb_MC(double[] M, double[] K, int[,] C)
        {
            var prob_MC = new double[M.Length, M.Length];
            for (int i = 0; i < M.Length; i++)
            {
                for (int j = 0; j < M.Length; j++)
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

        public List<int> Determinictic(List<List<double>> prob_MC_Cond) =>
            prob_MC_Cond.Select(el => el.IndexOf(el.Max())).ToList();

        public double AveLossDet(double[] D, double[,] prob_MC_Cond, double[] prob_C) =>
            prob_C
            .Select((el, i) => el * (1 - prob_MC_Cond[i, (int)D[i]]))
            .ToArray()
            .Sum();
        public List<List<double>> Stochastic(List<List<double>> prob_MC_Cond)
        {
            var D = new List<List<double>>();
            var maxList = new List<Tuple<double, int>>();
            prob_MC_Cond.ForEach(list =>
            {
                maxList.Add(Tuple.Create(list.Max(),
                        list.Count(el => el == list.Max())));
                 D.Add(new List<double>());
            });

            for (int i = 0; i < prob_MC_Cond.Count; i++)
            {
                for (int j = 0; j < prob_MC_Cond.Count; j++)
                {
                    D[i].Add(prob_MC_Cond[i][j] == maxList[i].Item1 ? 1.0 / maxList[i].Item2 : 0);
                }
            }

            return D;
        }

    }
}
