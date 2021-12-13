using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace ConsoleApp2
{
    class Files
    {
        public void DisplayTable<T>(T[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }

        public void DisplayArr<T>(List<T> arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine($"C[{i}] -> M[{arr[i]}]");
            }
        }

        public double[,] ReadTable(string filepath, int arrLength1, int arrLength2)
        {
            var table = new double[arrLength1, arrLength2];
            var contents = File.ReadAllText(filepath).Split('\n');
            var csv = from line in contents
                      select line.Split(',').ToArray();

            for (int i = 0; i < arrLength1; i++)
            {
                var csvList = csv.ToList();
                for (int j = 0; j < arrLength2; j++)
                {
                    table[i, j] = Convert.ToDouble((csvList[i][j]).Replace(".", ","));
                }
            }

            return table;
        }

        public int[,] Intify(double[,] arr)
        {
            var table = new int[arr.GetLength(0), arr.GetLength(1)];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    table[i, j] = (int)arr[i, j];
                }
            }

            return table;
        }

        public double[] ToLine(double[,] arr, int line)
        {
            var list = new double[arr.GetLongLength(1)];
            for (int i = 0; i < arr.GetLongLength(1); i++)
            {
                list[i] = arr[line, i];
            }

            return list;
        }

        public void DisplayList(List<List<double>> D)
        {
            for (int i = 0; i < D.Count; i++)
            {
                for (int j = 0; j < D.Count; j++)
                {
                    Console.Write(Math.Round(D[i][j], 4).ToString().PadLeft(6) + "|");
                }
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }




    }
}
