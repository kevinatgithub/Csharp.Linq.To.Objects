using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace LINQ.to.Objects.Directories
{
    class Example_15_CompareDirectories
    {
        public static void Run()
        {
            DirectoryInfo dir1 = new DirectoryInfo(@"C:\KEVIN\Master Class\notes1");
            DirectoryInfo dir2 = new DirectoryInfo(@"C:\KEVIN\Master Class\notes2");

            IEnumerable<FileInfo> list1 = dir1.GetFiles("*.*", SearchOption.AllDirectories);
            IEnumerable<FileInfo> list2 = dir2.GetFiles("*.*", SearchOption.AllDirectories);

            FileComparer fileComparer = new FileComparer();


            #region SequenceEqual
            bool isIdentical = list1.SequenceEqual(list2, fileComparer);

            if (isIdentical)
                Console.WriteLine("the two folders are the same");
            else
                Console.WriteLine("the two folders are not the same");

            #endregion

            Console.WriteLine();

            #region Intersect
            var commonFiles = list1.Intersect(list2, fileComparer);

            Console.WriteLine("The following files are in both folders:");
            foreach (var f in commonFiles)
            {
                Console.WriteLine($"\t{f.FullName}");
            }

            #endregion

            Console.WriteLine();

            #region Except
            var filesUniqueInFolder1 = (from file in list1
                                        select file).Except(list2, fileComparer);

            Console.WriteLine("The following files are in folder1 but not in folder2:");
            foreach (var f in filesUniqueInFolder1)
            {
                Console.WriteLine($"\t{f.FullName}");
            }
            #endregion

            Console.ReadLine();
        }
    }

    class FileComparer : IEqualityComparer<FileInfo>
    {
        public bool Equals([AllowNull] FileInfo x, [AllowNull] FileInfo y)
        {
            return (x.Name == y.Name && x.Length == y.Length);
        }

        public int GetHashCode([DisallowNull] FileInfo obj)
        {
            var s = $"{obj.Name}{obj.Length}";
            return s.GetHashCode();
        }
    }
}
