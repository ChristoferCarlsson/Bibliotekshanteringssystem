using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarysystem
{
    public class Book
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genres { get; set; }
        public int Publish { get; set; }
        public int ISBN { get; set; }
        public bool Borrowed { get; set; }
        public List<int> Reviews { get; set; }


        public Book(string title, string author, List<string> genres, int publish, int isbn, bool borrowed, List<int> reviews)
        {
            ID = 0;
            Title = title;
            Author = author;
            Genres = genres;
            Publish = publish;
            ISBN = isbn;
            Reviews = reviews;
            Borrowed = borrowed;
            Reviews = reviews;
        }
    }
}
