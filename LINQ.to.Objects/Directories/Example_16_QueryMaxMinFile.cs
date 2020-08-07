using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Directories
{
    class Example_16_QueryMaxMinFile
    {
        public static void Run()
        {
            DirectoryInfo dir1 = new DirectoryInfo(@"C:\KEVIN\Master Class\notes1");

            IEnumerable<FileInfo> files = dir1.GetFiles("*.*", SearchOption.AllDirectories);

            var largest = (from file in files
                        let len = GetFileLength(file)
                        orderby len descending
                        select file).First();

            Console.WriteLine($"The Largest file is : {largest.FullName}");
            Console.WriteLine();

            var smallest = (from file in files
                           let len = GetFileLength(file)
                           orderby len ascending
                           select file).First();

            Console.WriteLine($"The Smallest file is : {smallest.FullName}");
            Console.WriteLine();

            var top3 = (from file in files
                           let len = GetFileLength(file)
                           orderby len descending
                           select file).Take(3);

            Console.WriteLine($"The 3 Largest files are :");

            foreach (var f in top3)
            {
                Console.WriteLine($"\t{f.FullName}");
            }

            Console.ReadLine();
        }

        static long GetFileLength(System.IO.FileInfo fi)
        {
            long retval;
            try
            {
                retval = fi.Length;
            }
            catch (System.IO.FileNotFoundException)
            {
                // If a file is no longer present,  
                // just add zero bytes to the total.  
                retval = 0;
            }
            return retval;
        }
    }
}
