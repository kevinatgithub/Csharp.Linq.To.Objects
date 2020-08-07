using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_06_SortingCSVFiles
    {
        public static void Run()
        {
            var path = "../../../Strings/txt/";
            var file1 = "spreadsheet1.csv";

            string[] lines = File.ReadAllLines(path + file1);

            IEnumerable<string> query = from line in lines
                                        let x = line.Split(",")
                                        orderby x[2]
                                        select x[2].Trim() + ", " + (x[1] + " " + x[0]);

            var file2 = "spreadsheet2.csv";
            File.WriteAllLines(path + file2, query.ToArray());

            Console.WriteLine("spreadsheet2.csv written to disk");

            Console.ReadLine();
        }
    }
}
