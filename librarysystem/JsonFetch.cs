using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace librarysystem
{
     class JsonFetch
    {
        public static MyDatabase fetch()
        {
            string dataJSONfilPath = "LibraryData.json";
            string allaDataSomJSONType = File.ReadAllText(dataJSONfilPath);
            MyDatabase myDatabase = JsonSerializer.Deserialize<MyDatabase>(allaDataSomJSONType)!;

            return myDatabase;
        }
    }
}
