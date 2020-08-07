using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace LINQ.to.Objects.Directories
{
    class Example_12_FindFileByExtension
    {
        public static void Run()
        {
            var startFolder = @"C:\Users\KevinC\Downloads";

            var dir = new DirectoryInfo(startFolder);

            IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

            IEnumerable<FileInfo> fileQuery = from file in fileList
                                              where file.Extension == ".txt"
                                              orderby file.Name
                                              select file;

            foreach (var file in fileQuery)
            {
                Console.WriteLine(file.FullName);
            }

            Console.ReadLine();

            var newestFile = (from file in fileQuery
                              orderby file.CreationTime
                              select new { file.FullName, file.CreationTime }).Last();

            Console.WriteLine("\r\nThe newest .txt file is {0}. Creation time: {1}",
            newestFile.FullName, newestFile.CreationTime);

            Console.ReadLine();
        }
    }
}
