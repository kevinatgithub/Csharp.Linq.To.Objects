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
                "1" => typeof(Example_1_CountWordOccurence),
                "2" => typeof(Example_2_CountSentencesWithSetOfWords),
                "3" => typeof(Example_3_QueryWithRegex),
                "4" => typeof(Example_4_CompareList),
                "5" => typeof(Example_5_SortLines),
                "6" => typeof(Example_6_SortingCSVFiles),
                "7" => typeof(Example_7_MergeStrings),
                "8" => typeof(Example_8_JoinDataFromDifferentSources),
                "9" => typeof(Example_9_PopulateCollection),
                "10" => typeof(Example_10_SplitWithGroups),
                "11" => typeof(Example_11_ComputeColumnValues),
                "12" => typeof(Example_12_ReflectionHowTo),
                "13" => typeof(Example_13_FindFileByExtension),
                "14" => typeof(Example_14_GroupByExtension),
                "15" => typeof(Example_15_TotalNumberOfBytes),
                _ => null
            };

            return app?.GetMethod("Run")?.Invoke(null, null) ?? ExampleMenu();
        }
    }
}
