using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_5_SortLines
    {
        public static void Run()
        {
            string[] scores = File.ReadAllLines(@"../../../Strings/txt/scores.csv");

            int sortField = 1;

            Console.WriteLine("Sorted highest to lowest by field {0}", sortField);

            var sortQuery = from line in scores
                            let fields = line.Split(",")
                            orderby fields[sortField] descending
                            select line;

            foreach (string line in sortQuery)
            {
                Console.WriteLine(line);
            }

            Console.ReadLine();
        }
    }
}
