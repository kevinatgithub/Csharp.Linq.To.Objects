# Linq to Objects
    -Use of Linq to any IEnumerable / IEnumerable<T> collection directly. 
	-Without using any LINQ provider API such as LINQ to SQL or LINQ to XML.
	-Represents a new approach to collections instead of using foreach.
	-More declarative, concise and readable codes.
	-Provide powerful filtering, ordering, and grouping capabilities with less application code.
	-Can be ported to other data sources with little or no modification.

Source : [Microsoft Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/linq-to-objects)

# Linq to Strings
1. Count Word Occurence 
    - Split
```csharp
var words = lyrics.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

var query = from word in words
            where word.Contains(keyword, StringComparison.CurrentCultureIgnoreCase)
            select word;
```
2. Query for sentences that contain a specified set of words
    - let
    - Distinct
    - Intersect

```csharp
string[] sentences = lyrics.ToLowerInvariant().Split(new char[] { '.', '?', '!','\n','\r' });

var query = from sentence in sentences
            let words = sentence.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries)
            where words.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
            select sentence;
```
3. Query with Reqular Expression
    - use of regular expresion
    - use of anonymous type
```csharp
var searchTerm = new System.Text.RegularExpressions.Regex(@"Visual (Basic|C#|C\+\+|Studio)");

var queryMatchingFiles = from file in fileList
                         where file.Extension.Contains(".htm")
                         let fileText = File.ReadAllText(file.FullName)
                         let matches = searchTerm.Matches(fileText)
                         where matches.Count() > 0
                         select new
                         {
                             name = file.FullName,
                             matchedValues = from match in matches
                                             select match.Value
                         };
```
4. Compare List
    - Some types of query operations in C#, such as Except, Distinct, Union, and Concat, can only be expressed in method-based syntax.
```csharp
IEnumerable<string> differenceQuery = names1.Except(names2);
```
5. Sort Text
    - shows how to sort lines of structured text, such as comma-separated values, by any field in the line
	- orderby , descending
```csharp
int sortField = 1;
var sortQuery = from line in scores
                let fields = line.Split(",")
                orderby fields[sortField] descending
                select line;
```
6. Sort and Reorder Fields
	- sort fields
	- reorder fields
```csharp
IEnumerable<string> query = from line in lines
                            let x = line.Split(",")
                            orderby x[2]
                            select x[2].Trim() + ", " + (x[1] + " " + x[0]);
```
7. Combine and Compare String Collections
    - concat
    - unique
    - intersect
```csharp
IEnumerable<string> concatQuery = fileA.Concat(fileB).OrderBy(s => s);

IEnumerable<string> uniqueNamesQuery = fileA.Union(fileB).OrderBy(s => s);

IEnumerable<string> commonNamesQuery = fileA.Intersect(fileB);

string nameMatch = "Garcia";

IEnumerable<string> tempQuery1 = from name in fileA
                                 let n = name.Split(',')
                                 where n[0] == nameMatch
                                 select name;

IEnumerable<string> tempQuery2 = from name in fileB
                                 let n = name.Split(',')
                                 where n[0] == nameMatch
                                 select name;

IEnumerable<string> nameMatchQuery = tempQuery1.Concat(tempQuery2).OrderBy(s => s);
```
8. Join Data from different files
    - merge of in-memory data
    - dont try to join in-memory or in-file system with data in database
```csharp
string[] names = File.ReadAllLines(namesFilePath);
string[] scores = File.ReadAllLines(scoresFilePath);

IEnumerable<string> query = from nameLine in names
                             let splitName = nameLine.Split(',')
                             from scoreLine in scores
                             let splitScore = scoreLine.Split(',')
                             where Convert.ToInt32(splitName[2]) == Convert.ToInt32(splitScore[0])
                             select splitName[0]+ ", "+
                             splitScore[1] + ", "+
                             splitScore[2] + ", "+
                             splitScore[3] + ", ";
```
9. Compute Fields
    - Use of New Type
    - Use of ToList for faster query
    - Use of Aggregate Method Average

```csharp
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
```
10. Split with groups
```csharp
var file1 = File.ReadAllLines(filePath1);
var file2 = File.ReadAllLines(filePath2);

var mergeQuery = file1.Union(file2);

var groupQuery = from name in mergeQuery
                 let n = name.Split(',')
                 group name by n[0][0] into g
                 orderby g.Key
                 select g;
```

# Linq to Reflection

- Use of reflection API (supports generic IEnumerable interface)
- Use of Reflection Methods such as GetTypes. GetMethods etc.

```csharp
Assembly assembly = typeof(Example_11_ReflectionHowTo).Assembly;

var pubTypesQuery = from type in assembly.GetTypes()
                    where type.IsPublic
                    from method in type.GetMethods()
                    where method.ReturnType.IsArray == true
                        || (method.ReturnType.GetInterface(
                            typeof(System.Collections.Generic.IEnumerable<>).FullName) != null
                        && method.ReturnType.FullName != "System.String")
                    group method.ToString() by type.ToString();
```

# Linq to Directories
1. Find file by extension
    - Finding files by extension
    - Finding newest file
```csharp
var dir = new DirectoryInfo(startFolder);
IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);
IEnumerable<FileInfo> fileQuery = from file in fileList
                                  where file.Extension == ".txt"
                                  orderby file.Name
                                  select file;

var newestFile = (from file in fileQuery
                  orderby file.CreationTime
                  select new { file.FullName, file.CreationTime }).Last();
```
2. Group by file extension
```csharp
var dir = new DirectoryInfo(startFolder);
IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

var groupByExtensionQuery = from file in fileList
                            group file by file.Extension.ToLower() into fileGroup
                            orderby fileGroup.Key
                            select fileGroup;
```
3. Total Number of bytes
```csharp
IEnumerable<string> fileList = Directory.GetFiles(startFolder, "*.*", System.IO.SearchOption.AllDirectories);

var fileQuery = from file in fileList
                select GetFileLength(file);

long[] fileLengths = fileQuery.ToArray();

long largestFile = fileLengths.Max();

long totalBytes = fileLengths.Sum();
```
4. Compare Directories
```csharp
IEnumerable<FileInfo> list1 = dir1.GetFiles("*.*", SearchOption.AllDirectories);
IEnumerable<FileInfo> list2 = dir2.GetFiles("*.*", SearchOption.AllDirectories);

FileComparer fileComparer = new FileComparer();

bool isIdentical = list1.SequenceEqual(list2, fileComparer);

var commonFiles = list1.Intersect(list2, fileComparer);

var filesUniqueInFolder1 = (from file in list1
                            select file).Except(list2, fileComparer);
```
5. Query Largest / Smallest File
```csharp
IEnumerable<FileInfo> files = dir1.GetFiles("*.*", SearchOption.AllDirectories);

var largest = (from file in files
            let len = GetFileLength(file)
            orderby len descending
            select file).First();

var smallest = (from file in files
               let len = GetFileLength(file)
               orderby len ascending
               select file).First();

var top3 = (from file in files
               let len = GetFileLength(file)
               orderby len descending
               select file).Take(3);
```
