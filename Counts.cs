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

        public double AveLossDet(List<int> D, List<List<double>> prob_MC_Cond, double[] prob_C) =>
            prob_C
            .Select((el, i) => el * (1 - prob_MC_Cond[i][D[i]]))
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
                 D.Add(new List<double>(new double[20].ToList()));
            });

            D = prob_MC_Cond
                .Select(
                    (list, listInd) =>
                            list.Select(
                                (el, elInd) =>
                                       el == maxList[listInd].Item1 ? 1.0 / maxList[listInd].Item2 : 0)
                        .ToList())
                .ToList();

            return D;
        }

        public double AveLossStoch(List<List<double>> D, List<List<double>> prob_MC_Cond, double[] prob_C) =>
            prob_MC_Cond
                .Select((list, listInd) =>
                        (1 - list.Select((el, elInd) => el * D[listInd][elInd])
                            .ToArray()
                            .Sum()) * prob_C[listInd])
                .ToList()
                .Sum();

    }
}
