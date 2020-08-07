using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace LINQ.to.Objects.Directories
{
    class Example_14_TotalNumberOfBytes
    {
        public static void Run()
        {
            string startFolder = @"C:\KEVIN\Master Class";

            IEnumerable<string> fileList = Directory.GetFiles(startFolder, "*.*", System.IO.SearchOption.AllDirectories);

            var fileQuery = from file in fileList
                            select GetFileLength(file);

            long[] fileLengths = fileQuery.ToArray();

            long largestFile = fileLengths.Max();

            long totalBytes = fileLengths.Sum();

            Console.WriteLine("There are {0} bytes in {1} files under {2}",
                totalBytes, fileList.Count(), startFolder);
            Console.WriteLine("The largest files is {0} bytes.", largestFile);

            Console.ReadKey();
        }

        static long GetFileLength(string filename)
        {
            long retval;
            try
            {
                FileInfo fi = new System.IO.FileInfo(filename);
                retval = fi.Length;
            }
            catch (FileNotFoundException)
            {
                // If a file is no longer present,  
                // just add zero bytes to the total.  
                retval = 0;
            }
            return retval;
        }
    }
}
