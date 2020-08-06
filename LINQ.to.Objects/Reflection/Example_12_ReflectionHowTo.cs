using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace LINQ.to.Objects.Reflection
{
    class Example_12_ReflectionHowTo
    {
        public static void Run()
        {
            Assembly assembly = typeof(Example_12_ReflectionHowTo).Assembly;
                        
            var pubTypesQuery = from type in assembly.GetTypes()
                                where type.IsPublic
                                from method in type.GetMethods()
                                where method.ReturnType.IsArray == true
                                    || (method.ReturnType.GetInterface(
                                        typeof(System.Collections.Generic.IEnumerable<>).FullName) != null
                                    && method.ReturnType.FullName != "System.String")
                                group method.ToString() by type.ToString();

            foreach (var groupOfMethods in pubTypesQuery)
            {
                Console.WriteLine("Type: {0}", groupOfMethods.Key);
                foreach (var method in groupOfMethods)
                {
                    Console.WriteLine("  {0}", method);
                }
            }

            Console.WriteLine("Press any key to exit... ");
            Console.ReadKey();
        }
    }
}
