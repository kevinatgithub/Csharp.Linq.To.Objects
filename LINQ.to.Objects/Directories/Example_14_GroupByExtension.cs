using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace LINQ.to.Objects.Directories
{
    class Example_14_GroupByExtension
    {
        public static void Run()
        {
            var startFolder = @"C:\KEVIN\Master Class";

            int tremLength = startFolder.Length;

            var dir = new DirectoryInfo(startFolder);

            IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

            var groupByExtensionQuery = from file in fileList
                                        group file by file.Extension.ToLower() into fileGroup
                                        orderby fileGroup.Key
                                        select fileGroup;

            PageOutput(tremLength, groupByExtensionQuery);
        }

        private static void PageOutput(int tremLength, IOrderedEnumerable<IGrouping<string, FileInfo>> groupByExtensionQuery)
        {
            var goAgain = true;

            int numLines = Console.WindowHeight - 3;

            foreach (var fileGroup in groupByExtensionQuery)
            {
                int currentLine = 0;

                do
                {
                    Console.Clear();
                    Console.WriteLine(fileGroup.Key == String.Empty ? "[none]" : fileGroup.Key);

                    var resultPage = fileGroup.Skip(currentLine).Take(numLines);

                    foreach (var f in resultPage)
                    {
                        Console.WriteLine("\t{0}", f.FullName.Substring(tremLength));
                    }

                    currentLine += numLines;

                    Console.WriteLine("Press any key to continue or the 'End' key to break...");

                    ConsoleKey key = Console.ReadKey().Key;

                    if (key == ConsoleKey.End)
                    {
                        goAgain = false;
                        break;
                    }
                } while (currentLine < fileGroup.Count());
                if (goAgain == false)
                    break;
            }
        }
    }
}
