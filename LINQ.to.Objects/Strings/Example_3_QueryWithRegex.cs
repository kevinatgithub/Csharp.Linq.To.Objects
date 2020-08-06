using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_3_QueryWithRegex
    {
        public static void Run()
        {
            var startFolder = @"C:\Program Files (x86)\Microsoft Visual Studio\";

            IEnumerable<FileInfo> fileList = GetFiles(startFolder);
            var searchTerm = new System.Text.RegularExpressions.Regex(@"Visual (Basic|C#|C\+\+|Studio)");

            var queryMatchingFiles = from file in fileList
                                     where file.Extension.Contains(".htm")
                                     let fileText = File.ReadAllText(file.FullName)
                                     let matches = searchTerm.Matches(fileText)
                                     where matches.Count() > 0
                                     select new
                                     {
                                         name = file.FullName,
                                         matchedValues = from match in matches
                                                         select match.Value
                                     };

            Console.WriteLine("The term \"{0}\" was found in:", searchTerm.ToString());

            foreach(var v in queryMatchingFiles)
            {
                var s = v.name.Substring(startFolder.Length - 1);
                Console.WriteLine(s);

                foreach(var v2 in v.matchedValues)
                {
                    Console.WriteLine(" " + v2);
                }
            }

            Console.ReadLine();
        }

        private static IEnumerable<FileInfo> GetFiles(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();

            string[] fileNames = null;
            List<FileInfo> files = new List<FileInfo>();

            fileNames = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach (string name in fileNames)
            {
                files.Add(new FileInfo(name));
            }
            return files;
        }
    }
}
