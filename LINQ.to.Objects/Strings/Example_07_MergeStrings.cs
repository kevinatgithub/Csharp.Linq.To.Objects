using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_07_MergeStrings
    {
        public static void Run()
        {
            string path = "../../../Strings/txt/";
            string file1 = path + "names1.txt";
            string file2 = path + "names2.txt";


            string[] fileA = File.ReadAllLines(file1);
            string[] fileB = File.ReadAllLines(file2);

            IEnumerable<string> concatQuery = fileA.Concat(fileB).OrderBy(s => s);

            OutputQueryResults(concatQuery, "Simple concatenate and sort. Duplicates are preserved:");

            IEnumerable<string> uniqueNamesQuery = fileA.Union(fileB).OrderBy(s => s);

            OutputQueryResults(uniqueNamesQuery, "Union removes duplicate names:");

            IEnumerable<string> commonNamesQuery = fileA.Intersect(fileB);

            OutputQueryResults(commonNamesQuery, "Merge based on intersect:");

            string nameMatch = "Garcia";

            IEnumerable<string> tempQuery1 = from name in fileA
                                             let n = name.Split(',')
                                             where n[0] == nameMatch
                                             select name;

            IEnumerable<string> tempQuery2 = from name in fileB
                                             let n = name.Split(',')
                                             where n[0] == nameMatch
                                             select name;

            IEnumerable<string> nameMatchQuery = tempQuery1.Concat(tempQuery2).OrderBy(s => s);

            OutputQueryResults(nameMatchQuery, $"Concat based on partial name match \"{nameMatch}\"");

            Console.ReadLine();

            Console.ReadLine();
        }

        private static void OutputQueryResults(IEnumerable<string> query, string message)
        {
            Console.WriteLine(Environment.NewLine + message);

            foreach(string item in query)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("{0} total names in list", query.Count());
        }
    }
}
