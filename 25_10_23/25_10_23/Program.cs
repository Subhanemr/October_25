using Newtonsoft.Json;

namespace _25_10_23
{
    internal class Program
    {
        private static List<string> Names = new List<string>();

        #region Non-Dynamic

        //Non-Dynamic File Path

        //private static string DbPath = @"C:\Users\T x\Desktop\Control structure(Conditional)\25_10_23\25_10_23\25_10_23\Files\name.json";

        #endregion

        #region Dynamic

        //Dynamic File Path

        private static string DbPath = Path.GetTempPath();

        #endregion

        static void Main(string[] args)
        {
            #region Dynamic

            //Dynamic File Path

            string databasePath = Path.GetTempPath();
            string jsonFilePath = "names.json";
            DbPath = databasePath + jsonFilePath;
            Console.WriteLine($"Json File is here: {DbPath}");

            #endregion

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
            if (!File.Exists(DbPath))
            {
                File.WriteAllText(DbPath, "[]");
            }
            string jsonData = JsonConvert.SerializeObject(Names, Formatting.Indented);
            File.WriteAllText(DbPath, jsonData);
        }

        static List<string> ReadFromDb()
        {
            string jsonData = File.ReadAllText(DbPath);
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