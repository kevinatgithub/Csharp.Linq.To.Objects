using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_10_SplitWithGroups
    {
        public static void Run()
        {
            var path = @"../../../Strings/txt/";
            var filePath1 = path + @"names1.txt";
            var filePath2 = path + @"names2.txt";
            var path2 = path + "names/";
            
            var file1 = File.ReadAllLines(filePath1);
            var file2 = File.ReadAllLines(filePath2);

            var mergeQuery = file1.Union(file2);

            var groupQuery = from name in mergeQuery
                             let n = name.Split(',')
                             group name by n[0][0] into g
                             orderby g.Key
                             select g;

            if (!Directory.Exists(path2))
                Directory.CreateDirectory(path2);

            foreach (var g in groupQuery)
            {
                var filePath3 = path2 + "names-" + g.Key + ".txt";
                Console.WriteLine(g.Key);

                using (StreamWriter sw = new StreamWriter(filePath3))
                {
                    foreach (var item in g)
                    {
                        sw.WriteLine(item);
                        Console.WriteLine("    {0}", item);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
