using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LINQ.to.Objects.Strings
{
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ID { get; set; }
        public List<int> ExamScores { get; set; }
    }

    class Example_09_PopulateCollection
    {
        public static void Run()
        {
            string path = "../../../Strings/txt/";
            string namesFilePath = path + "names.csv";
            string scoresFilePath = path + "scores.csv";

            string[] names = File.ReadAllLines(namesFilePath);
            string[] scores = File.ReadAllLines(scoresFilePath);

            IEnumerable<Student> studentsQuery = from nameLine in names
                                            let splitName = nameLine.Split(',')
                                            from scoreLine in scores
                                            let splitScore = scoreLine.Split(',')
                                            where Convert.ToInt32(splitName[2]) == Convert.ToInt32(splitScore[0])
                                            select new Student()
                                            {
                                                FirstName = splitName[0],
                                                LastName = splitName[1],
                                                ID = Convert.ToInt32(splitName[2]),
                                                ExamScores = (from scoresAsText in splitScore.Skip(1)
                                                              select Convert.ToInt32(scoresAsText)).ToList()
                                            };

            List<Student> students = studentsQuery.ToList();

            foreach(var s in students)
            {
                Console.WriteLine("The average score of {0} {1} is {2}",
                    s.FirstName,
                    s.LastName,
                    s.ExamScores.Average());
            }

            Console.ReadLine();
        }
    }
}
