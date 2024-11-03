using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using librarysystem;

namespace librarysystem
{

    public class Library
    {
    
        string dataJSONfilPath = "LibraryData.json";
        JsonFetch JsonFetch = new JsonFetch();
        MyDatabase myDatabase = JsonFetch.fetch();

        public void CreateBook(string title, string author, List<string> genres, int publish, int isbn, List<string> reviews)
        {
            List<Book> allBooks = myDatabase.allBooksFromDB;
            allBooks.Add(new Book(allBooks.Count, title, author, genres, publish, isbn, false, reviews));

            string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataJSONfilPath, updatedJSON);
        }

        public void CreateAuthor(string name, string description)
        {
            name = CleanUp(name);
            List<Author> allAuthors = myDatabase.allAuthorsFromDB;

            allAuthors.Add(new Author(name, description));

            string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataJSONfilPath, updatedJSON);
        }

        public void SearchBook(string search,string name)
        {
            name = CleanUp(name);
            List<Book> allBooks = myDatabase.allBooksFromDB;

            if (search == "author")
            {
                var authorBooks = allBooks.Where(n => n.Author == name).ToList();

                if (authorBooks.Count > 0)
                {
                    Console.WriteLine("Inga böcker fanns av den här författaren");
                }
                else
                {
                    authorBooks.ForEach(book => Console.WriteLine(book.Title));
                }

            } 
            else
            {
                var titleBook = allBooks.Where(n => n.Title == name).ToList();

                if (titleBook.Count == 0)
                {
                    Console.WriteLine("Den här boken finns inte i biblioteket");
                }
                else
                {
                    titleBook.ForEach(book => Console.WriteLine(book.Title));
                }
            }
        }
        public void FilterGenre(string genre)
        {
            genre = CleanUp(genre);
            List<Book> allBooks = myDatabase.allBooksFromDB;
            var genreBooks = allBooks.Where(x => x.Genres.Any(y => y == genre)).ToList();

            if (genreBooks.Count == 0)
            {
                Console.WriteLine("Den här genren finns inte i biblioteket");
            }
            else
            {
                genreBooks.ForEach(book => Console.WriteLine(book.Title));
            }
        }


        public void Sort(string sort)
        {
            List<Book> allBooks = myDatabase.allBooksFromDB;


            if (sort == "publish")
            {
                var sortAfter = allBooks.OrderBy(book => book.Publish).ToList();
                sortAfter.ForEach(book => Console.WriteLine(book.Title + " " + book.Publish));
            } else
            {
                var sortAfter = allBooks.OrderBy(book => book.Author).ToList();
                sortAfter.ForEach(book => Console.WriteLine(book.Title + " " + book.Author));
            }
        }

        public void RemoveBook(string title)
        {
            List<Book> allBooks = myDatabase.allBooksFromDB;
            allBooks.RemoveAll(x => x.Title == title);

            string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataJSONfilPath, updatedJSON);
        }

        public void RemoveAuthor(string name)
        {
            List<Author> allAuthors = myDatabase.allAuthorsFromDB;
            allAuthors.RemoveAll(x => x.Name == name);

            string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataJSONfilPath, updatedJSON);
        }

        public void UpdateBook(string name)
        {

            name = CleanUp(name);
            List<Book> allBooks = myDatabase.allBooksFromDB;
            var titleBook = allBooks.Where(n => n.Title == name).ToList();

            Console.WriteLine("Vad heter boken?");
            string title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Var vänlig och fyll i alla fält!");
                return;
            }

            Console.WriteLine("Vad heter författaren?");
            string author = Console.ReadLine();
            if (string.IsNullOrEmpty(author))
            {
                Console.WriteLine("Var vänlig och fyll i alla fält!");
                return;
            }

            Console.WriteLine("När var den publiserad?");
            string inp = Console.ReadLine();
            int publish;
            try
            {
                publish = Int32.Parse(inp);
            }
            catch (FormatException)
            {
                Console.WriteLine($"{inp} är inget nummer!");
                return;
            }

            title = CleanUp(title);
            author = CleanUp(author);

            titleBook[0].Title = title;
            titleBook[0].Author = author;
            titleBook[0].Publish = publish;


            string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataJSONfilPath, updatedJSON);
        }

        public void PrintBooks()
        {
            List<Book> allBooks = myDatabase.allBooksFromDB;
            allBooks.ForEach(book => Console.WriteLine(book.Title));
        }

        public void PrintAuthors()
        {
            List<Author> allAuthors = myDatabase.allAuthorsFromDB;
            allAuthors.ForEach(author => Console.WriteLine(author.Name));
        }

        private static string CleanUp(string inputString)
        {
            inputString = inputString.ToLower();
            // Split the input string into words, capitalize the first character of each word, and join them back into a string
            return string.Join(" ", inputString.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1)));
        }
    }
}
