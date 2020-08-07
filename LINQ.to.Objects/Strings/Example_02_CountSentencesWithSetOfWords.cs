using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Example_02_CountSentencesWithSetOfWords
    {
        public static void Run()
        {
            string lyrics = System.IO.File.ReadAllText(@"../../../Strings/txt/lyrics2.txt");

            string[] wordsToMatch = { "then", "make", "better"};

            string[] sentences = lyrics.ToLowerInvariant().Split(new char[] { '.', '?', '!','\n','\r' });

            var query = from sentence in sentences
                        let words = sentence.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries)
                        where words.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                        select sentence;

            foreach(var s in query)
            {
                Console.WriteLine(s);
            }

            Console.ReadLine();
        }
    }
}
