using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_11_ComputeColumnValues
    {
        public static void Run()
        {
            var path = @"../../../Strings/txt/scores.csv";
            var lines = File.ReadAllLines(path);

            int exam = 3;

            SingleColumn(lines, exam + 1);

            Console.WriteLine();

            MultipleColumn(lines);

            Console.ReadLine();
        }

        private static void MultipleColumn(string[] strs)
        {
            Console.WriteLine("Multi Column Query:");

            IEnumerable<IEnumerable<int>> multiColumnQuery = from line in strs
                                                             let elems = line.Split(',')
                                                             let scores = elems.Skip(1)
                                                             select (from str in scores
                                                                     select Convert.ToInt32(str));

            var result = multiColumnQuery.ToList();

            int columnCount = result[0].Count();

            for(int column = 0; column < columnCount; column++)
            {
                var result2 = from row in result
                              select row.ElementAt(column);

                var result2List = result2.ToList();

                double ave = result2List.Average();
                int max = result2List.Max();
                int min = result2List.Min();

                Console.WriteLine("Exam #:{0}    Average:{1:##.##}    High Score:{2}    Low Score:{3}",
                column + 1, ave, max, min);
            }
        }

        private static void SingleColumn(string[] strs, int exam)
        {
            Console.WriteLine("Single Column Query:");
            var columnQuery = from line in strs
                              let elements = line.Split(',')
                              select Convert.ToInt32(elements[exam]);

            var result = columnQuery.ToList();

            double ave = result.Average();
            int max = result.Max();
            int min = result.Min();

            Console.WriteLine("Exam #:{0}    Average:{1:##.##}    High Score:{2}    Low Score:{3}",
                exam, ave, max, min);
        }
    }
}
