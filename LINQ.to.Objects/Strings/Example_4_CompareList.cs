using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_4_CompareList
    {
        private const string path1 = "../../../Strings/txt/names1.txt";
        private const string path2 = "../../../Strings/txt/names2.txt";

        public static void Run()
        {
            if (!File.Exists(path1) || !File.Exists(path2))
                throw new FileNotFoundException();

            string[] names1 = File.ReadAllLines(path1);
            string[] names2 = File.ReadAllLines(path2);

            IEnumerable<string> differenceQuery = names1.Except(names2);

            Console.WriteLine("The following lines are in names1.txt but not names2.txt");
            foreach (string s in differenceQuery)
                Console.WriteLine(s);

            Console.ReadLine();
        }
    }
}
