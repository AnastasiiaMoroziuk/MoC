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

        public void DisplayArr<T>(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine("");
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

        public void AddToExcel<T>(string sheetName, string filename, T[,] arr)
        {

            var excelApp = new Excel.Application();
            var workbook = excelApp.Workbooks.Add();

            Excel._Worksheet sheet = (Excel._Worksheet)excelApp.ActiveSheet;
            
            FileInfo file = new FileInfo(@"C:\"+filename);

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    sheet.Cells[i + 1, j + 1] = arr[i, j].ToString();
                }
            }

            workbook.SaveAs(file);
            excelApp.Quit();
        }



    }
}
