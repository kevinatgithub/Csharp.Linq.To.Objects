using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_08_JoinDataFromDifferentSources
    {
        public static void Run()
        {
            string path = "../../../Strings/txt/";
            string namesFilePath = path + "names.csv";
            string scoresFilePath = path + "scores.csv";

            string[] names = File.ReadAllLines(namesFilePath);
            string[] scores = File.ReadAllLines(scoresFilePath);

            IEnumerable<string> query = from nameLine in names
                                                 let splitName = nameLine.Split(',')
                                                 from scoreLine in scores
                                                 let splitScore = scoreLine.Split(',')
                                                 where Convert.ToInt32(splitName[2]) == Convert.ToInt32(splitScore[0])
                                                 select splitName[0]+ ", "+
                                                 splitScore[1] + ", "+
                                                 splitScore[2] + ", "+
                                                 splitScore[3] + ", ";

            OutputQueryResults(query, "Merge two spreadsheets:");

            Console.ReadLine();
        }

        private static void OutputQueryResults(IEnumerable<string> query, string message)
        {
            Console.WriteLine(Environment.NewLine + message);
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("{0} Total names in list", query.Count());
        }
    }
}
