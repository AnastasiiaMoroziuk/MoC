using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp2
{
    class Counts
    {
        public List<double> CountProb_C(List<double> M, List<double> K, int[,] C)
        {

            var prob_C = new double[M.Count];

            for (int i = 0; i < M.Count; i++)
            {
                for (int j = 0; j < M.Count; j++)
                {
                    prob_C[C[i, j]] += K[i] * M[j];
                }
            }

            return prob_C.ToList();
        }



        public double[,] CountProb_MC(List<double> M, List<double> K, int[,] C)
        {
            var prob_MC = new double[M.Count, M.Count];
            for (int i = 0; i < M.Count; i++)
            {
                for (int j = 0; j < M.Count; j++)
                {
                    prob_MC[C[i, j], j] += K[i] * M[j];
                }
            }

            return prob_MC;
        }

        public List<List<double>> CountProb_MC_Conditional(List<double> prob_C, double[,] prob_MC)
        {
            var prob_MC_Cond = new List<List<double>>();

            for (int i = 0; i < prob_C.Count; i++)
            {
                prob_MC_Cond.Add(new List<double>());
                for (int j = 0; j < prob_C.Count; j++)
                {
                    prob_MC_Cond[i].Add(prob_MC[i,j]/prob_C[i]);
                }
            }

            return prob_MC_Cond;
        }

        public List<int> Determinictic(List<List<double>> prob_MC_Cond) =>
            prob_MC_Cond.Select(el => el.IndexOf(el.Max())).ToList();

        public double AveLossDet(List<int> D, List<List<double>> prob_MC_Cond, List<double> prob_C) =>
            prob_C
            .Select((el, i) => el * (1 - prob_MC_Cond[i][D[i]]))
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

        public double AveLossStoch(List<List<double>> D, List<List<double>> prob_MC_Cond, List<double> prob_C) =>
            prob_MC_Cond
                .Select((list, listInd) =>
                        (1 - list.Select((el, elInd) => el * D[listInd][elInd])
                            .Sum()) * prob_C[listInd])
                .Sum();

    }
}
