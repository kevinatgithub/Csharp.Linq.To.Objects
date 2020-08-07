using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_01_CountWordOccurence
    {
        public static void Run()
        {
            var lyrics = System.IO.File.ReadAllText(@"../../../Strings/txt/lyrics1.txt");

            var keyword = "binibini";

            var words = lyrics.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

            var query = from word in words
                        where word.Contains(keyword, StringComparison.CurrentCultureIgnoreCase)
                        select word;

            Console.WriteLine("There are {0} word occurence(s) for the word \"{1}\".", query.Count(), keyword);

            Console.ReadLine();

        }
    }
}
