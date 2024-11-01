using librarysystem;
using System.Text.Json;

namespace Librarysystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string dataJSONfilPath = "LibraryData.json";
            string allaDataSomJSONType = File.ReadAllText(dataJSONfilPath);

            MyDatabase myDatabase = JsonSerializer.Deserialize<MyDatabase>(allaDataSomJSONType)!;
        }

    }
}