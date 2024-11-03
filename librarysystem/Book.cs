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
        public List<string> Reviews { get; set; }


        public Book(int id, string title, string author, List<string> genres, int publish, int isbn, bool borrowed, List<string> reviews)
        {
            ID = id;
            Title = title;
            Author = author;
            Genres = genres;
            Publish = publish;
            ISBN = isbn;
            Borrowed = borrowed;
            Reviews = reviews;
        }
    }
}
