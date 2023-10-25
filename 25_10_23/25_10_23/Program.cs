using Newtonsoft.Json;

namespace _25_10_23
{
    internal class Program
    {
        private static List<string> Names = new List<string>();
        private static string databasePath = @"C:\Users\T x\Desktop\Control structure(Conditional)\25_10_23\25_10_23\25_10_23\Files\name.json";
        static void Main(string[] args)
        {
            Names = new List<string>
            {
                "Tyler",
                "Patrick",
                "Bob",
                "Ken",
                "John"
            };

            WriteToDb();

            bool found = Search(name => name.ToLower().Contains("a"));
            Console.WriteLine("Found: " + found);

            Delete("Bob");
        }

        static void WriteToDb()
        {
            if (!File.Exists(databasePath))
            {
                File.WriteAllText(databasePath, "[]");
            }
            string jsonData = JsonConvert.SerializeObject(Names, Formatting.Indented);
            File.WriteAllText(databasePath, jsonData);
        }

        static List<string> ReadFromDb()
        {
            string jsonData = File.ReadAllText(databasePath);
            return JsonConvert.DeserializeObject<List<string>>(jsonData);
        }

        static void Add(string name)
        {
            Names = ReadFromDb();
            Names.Add(name);
            WriteToDb();
        }

        static void Delete(string name)
        {
            ReadFromDb();
            Names.Remove(name);
            WriteToDb();
        }

        static bool Search(Predicate<string> predicate)
        {
            Names = ReadFromDb();
            return Names.Exists(predicate);
        }
    }
}