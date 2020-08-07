using System;
using LINQ.to.Objects.Strings;
using LINQ.to.Objects.Reflection;
using LINQ.to.Objects.Directories;

namespace LINQ.to.Objects
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleMenu();
        }

        private static object ExampleMenu()
        {
            Console.Clear();
            Console.WriteLine("Please enter example #:");
            var exampleId = Console.ReadLine();

            var app = exampleId switch
            {
                "1" => typeof(Example_01_CountWordOccurence),
                "2" => typeof(Example_02_CountSentencesWithSetOfWords),
                "3" => typeof(Example_03_QueryWithRegex),
                "4" => typeof(Example_04_CompareList),
                "5" => typeof(Example_05_SortLines),
                "6" => typeof(Example_06_SortingCSVFiles),
                "7" => typeof(Example_07_MergeStrings),
                "8" => typeof(Example_08_JoinDataFromDifferentSources),
                "9" => typeof(Example_09_PopulateCollection),
                "10" => typeof(Example_10_SplitWithGroups),
                "11" => typeof(Example_11_ReflectionHowTo),
                "12" => typeof(Example_12_FindFileByExtension),
                "13" => typeof(Example_13_GroupByExtension),
                "14" => typeof(Example_14_TotalNumberOfBytes),
                "15" => typeof(Example_15_CompareDirectories),
                "16" => typeof(Example_16_QueryMaxMinFile),
                _ => null
            };

            return app?.GetMethod("Run")?.Invoke(null, null) ?? ExampleMenu();
        }
    }
}
