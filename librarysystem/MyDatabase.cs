using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace librarysystem
{
    public class MyDatabase
    {
        [JsonPropertyName("books")]
        public List<Book> allBooksFromDB { get; set; }


        [JsonPropertyName("authors")]
        public List<Author> allAuthorsFromDB { get; set; }
    }
}
