using System.Text.Json;

namespace librarysystem
{
    public class Library
    {
        //Vi sätter upp våran JSON fil
        string dataJSONfilPath = "LibraryData.json";
        JsonFetch JsonFetch = new JsonFetch();
        MyDatabase myDatabase = JsonFetch.fetch();

        public void CreateBook(string title, string author, List<string> genres, int publish, int isbn)
        {
            //Vi gör så att allt är skrivet med rätt storlek på bokstäver.
            title = CleanUp(title);
            author = CleanUp(author);

            for (int i = 0; i < genres.Count; i++)
            {
                genres[i] = CleanUp(genres[i]);
            }

            //Vi gör en kopia av listan för att spara den nya boken i.
            List<Book> allBooks = myDatabase.allBooksFromDB;

            //Vi kollar om boken redan finns.
            var titleBook = allBooks.Where(n => n.Title == title).ToList();
            var isbnBook = allBooks.Where(n => n.ISBN == isbn).ToList();
            if (titleBook.Count > 0)
            {
                Console.WriteLine("Den här boken finns redan!");
                return;
            }
            else if (isbnBook.Count > 0)
            {
                Console.WriteLine("Den här ISBN finns redan registrerad!");
                return;
            }
            else
            {
                //Om boken inte redan finns, så läggs den till i listan.
                allBooks.Add(new Book(allBooks.Count, title, author, genres, publish, isbn, false, []));

                //Om författaren inte finns så skapas en ny i listan.
                List<Author> allAuthors = myDatabase.allAuthorsFromDB;
                var authors = allAuthors.Where(n => n.Name == author).ToList();
                if (authors.Count == 0) CreateAuthor(author, "");

                string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dataJSONfilPath, updatedJSON);

                Console.WriteLine("Boken är nu tillagd!");
            }
        }

        public void CreateAuthor(string name, string description)
        {
            name = CleanUp(name);
            List<Author> allAuthors = myDatabase.allAuthorsFromDB;

            //Vi kollar om författaren redan finns.
            var authors = allAuthors.Where(n => n.Name == name).ToList();
            if (authors.Count > 0)
            {
                Console.WriteLine("Den här författaren finns redan!");
                return;
            }
            else
            {
                allAuthors.Add(new Author(name, description));

                string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dataJSONfilPath, updatedJSON);

                Console.WriteLine("Författaren är nu tillagd!");
            }
        }

        public void SearchBook(string search, string name)
        {
            name = CleanUp(name);
            List<Book> allBooks = myDatabase.allBooksFromDB;
            List<Author> allAuthors = myDatabase.allAuthorsFromDB;

            //Om användaren söker via titel eller författaren
            if (search == "author")
            {
                //Finns författaren?
                var authorList = allAuthors.Where(n => n.Name == name).ToList();

                authorList.ForEach(author =>
                {
                    Console.WriteLine(author.Name);

                    if (author.Description.Length > 0)
                    {
                        Console.WriteLine(author.Description);
                    }
                    else
                    {
                        Console.WriteLine("Ingen beskrivning.");
                    }

                    var authorBooks = allBooks.Where(n => n.Author == author.Name).ToList();

                    //Om författaren har skrivit böcker
                    if (authorBooks.Count > 0)
                    {
                        Console.WriteLine("Författaren skrev också följande böcker:");
                        Console.WriteLine();
                        authorBooks.ForEach(authBook =>
                        {
                            Console.WriteLine($"{authBook.Title} skriven år {authBook.Publish}");
                            if (authBook.Reviews.Count > 0)
                            {
                                Console.WriteLine($"Betyg: {authBook.Reviews.Average()}");
                            }
                            Console.WriteLine();
                        });
                    }

                    Console.WriteLine();
                });
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

                    titleBook.ForEach(book =>
                    {
                        Console.WriteLine($"{book.Title} skriven av {book.Author}");
                        Console.WriteLine($"Skriven år {book.Publish}");
                        book.Genres.ForEach(genre => Console.WriteLine(genre));
                        Console.WriteLine($"ISBN {book.ISBN}");

                        if (book.Reviews.Count > 0)
                        {
                            Console.WriteLine($"Betyg: {book.Reviews.Average()}");
                        }
                        Console.WriteLine();
                    });
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

                Console.WriteLine("Böcker");
                Console.WriteLine("_______________");
                Console.WriteLine("");

                genreBooks.ForEach(book =>
                {
                    Console.WriteLine($"{book.Title} skriven av {book.Author}");
                    Console.WriteLine($"Skriven år {book.Publish}");
                    book.Genres.ForEach(genre => Console.WriteLine(genre));
                    Console.WriteLine($"ISBN {book.ISBN}");

                    if (book.Reviews.Count > 0)
                    {
                        Console.WriteLine($"Betyg: {book.Reviews.Average()}");
                    }
                    Console.WriteLine();
                });
            }
        }


        public void Sort(string sort)
        {
            List<Book> allBooks = myDatabase.allBooksFromDB;

            Console.WriteLine("Böcker");
            Console.WriteLine("_______________");
            Console.WriteLine("");

            if (sort == "publish")
            {
                var sortAfter = allBooks.OrderBy(book => book.Publish).ToList();
                sortAfter.ForEach(book =>
                {
                    Console.WriteLine($"{book.Title} skriven av {book.Author}");
                    Console.WriteLine($"Skriven år {book.Publish}");
                    book.Genres.ForEach(genre => Console.WriteLine(genre));
                    Console.WriteLine($"ISBN {book.ISBN}");

                    if (book.Reviews.Count > 0)
                    {
                        Console.WriteLine($"Betyg: {book.Reviews.Average()}");
                    }
                    Console.WriteLine();
                });
            }
            else
            {
                var sortAfter = allBooks.OrderBy(book => book.Author).ToList();
                sortAfter.ForEach(book =>
                {
                    Console.WriteLine($"{book.Title} skriven av {book.Author}");
                    Console.WriteLine($"Skriven år {book.Publish}");
                    book.Genres.ForEach(genre => Console.WriteLine(genre));
                    Console.WriteLine($"ISBN {book.ISBN}");

                    if (book.Reviews.Count > 0)
                    {
                        Console.WriteLine($"Betyg: {book.Reviews.Average()}");
                    }
                    Console.WriteLine();
                });
            }
        }

        public void RemoveBook(string title)
        {
            title = CleanUp(title);
            List<Book> allBooks = myDatabase.allBooksFromDB;
            allBooks.RemoveAll(x => x.Title == title);

            string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataJSONfilPath, updatedJSON);

            Console.WriteLine("Boken är nu borttagen!");
        }

        public void RemoveAuthor(string name)
        {
            name = CleanUp(name);
            List<Author> allAuthors = myDatabase.allAuthorsFromDB;
            allAuthors.RemoveAll(x => x.Name == name);

            string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataJSONfilPath, updatedJSON);

            Console.WriteLine("Författaren är nu borttagen!");
        }

        public void UpdateBook(string name)
        {
            //På grund av tidsbrist och att jag vill att koden inte skall vara för bloated, så kan man bara redigera 3 saker
            name = CleanUp(name);
            List<Book> allBooks = myDatabase.allBooksFromDB;
            var titleBook = allBooks.Where(n => n.Title == name).ToList();

            if (titleBook.Count == 0)
            {
                Console.WriteLine("Den här boken finns inte i biblioteket");
                return;
            }
            else
            {
                Console.WriteLine("Skriv den uppdaterade informationen");
                Console.WriteLine("Vad heter boken?");
                string title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Var vänlig och fyll i alla fält!");
                    return;
                }

                //Vi kollar om boken redan finns.
                var newBookName = allBooks.Where(n => n.Title == title).ToList();
                if (newBookName.Count > 0)
                {
                    Console.WriteLine("Den här boken finns redan!");
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

                //Det kan bara finnas en av samma bok, därför använder vi index [0]
                titleBook[0].Title = title;
                titleBook[0].Author = author;
                titleBook[0].Publish = publish;


                string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dataJSONfilPath, updatedJSON);

                Console.WriteLine("Boken är nu uppdaterad!");

            }
        }

        public void UpdateAuthor(string name)
        {
            name = CleanUp(name);
            List<Author> allAuthors = myDatabase.allAuthorsFromDB;
            var author = allAuthors.Where(n => n.Name == name).ToList();

            if (author.Count == 0)
            {
                Console.WriteLine("Den här författaren finns inte i biblioteket");
                return;
            }
            else
            {
                Console.WriteLine("Skriv den uppdaterade informationen");
                Console.WriteLine("Vad heter författaren?");
                string newName = Console.ReadLine();
                if (string.IsNullOrEmpty(newName))
                {
                    Console.WriteLine("Var vänlig och fyll i alla fält!");
                    return;
                }

                Console.WriteLine("Skriv en beskrivning om författaren.");
                string description = Console.ReadLine();
                if (string.IsNullOrEmpty(description))
                {
                    Console.WriteLine("Var vänlig och fyll i alla fält!");
                    return;
                }

                newName = CleanUp(newName);

                //Det kan bara finnas en av samma författare, därför använder vi index [0]
                author[0].Name = newName;
                author[0].Description = description;

                string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dataJSONfilPath, updatedJSON);

                Console.WriteLine("Författaren är nu uppdaterad!");

            }
        }

        public void SetReview(string title, int review)
        {
            title = CleanUp(title);
            List<Book> allBooks = myDatabase.allBooksFromDB;
            var titleBook = allBooks.Where(n => n.Title == title).ToList();

            if (titleBook.Count == 0)
            {
                Console.WriteLine("Den här boken finns inte i biblioteket");
                return;
            }
            else
            {
                titleBook[0].Reviews.Add(review);

                string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dataJSONfilPath, updatedJSON);

                Console.WriteLine("Betyget är nu satt!");
            }

        }
        public void PrintBooks()
        {
            //Vi skriver ut alla böcker
            List<Book> allBooks = myDatabase.allBooksFromDB;

            Console.WriteLine("Böcker");
            Console.WriteLine("_______________");
            Console.WriteLine("");
            allBooks.ForEach(book =>
            {
                Console.WriteLine($"{book.Title} skriven av {book.Author}");
                Console.WriteLine($"Skriven år {book.Publish}");
                book.Genres.ForEach(genre => Console.WriteLine(genre));
                Console.WriteLine($"ISBN {book.ISBN}");

                if (book.Reviews.Count > 0)
                {
                    Console.WriteLine($"Betyg: {book.Reviews.Average()}");
                }
                Console.WriteLine();
            });
        }

        public void PrintAuthors()
        {
            //Vi skriver ut alla författare
            List<Author> allAuthors = myDatabase.allAuthorsFromDB;

            List<Book> allBooks = myDatabase.allBooksFromDB;

            Console.WriteLine("Författare");
            Console.WriteLine("_______________");
            Console.WriteLine("");

            allAuthors.ForEach(author =>
            {
                Console.WriteLine(author.Name);

                if (author.Description.Length > 0)
                {
                    Console.WriteLine(author.Description);
                }
                else
                {
                    Console.WriteLine("Ingen beskrivning.");
                }

                var authorBooks = allBooks.Where(n => n.Author == author.Name).ToList();

                //Om författaren har skrivit böcker
                if (authorBooks.Count > 0)
                {
                    Console.WriteLine("Författaren skrev också följande böcker:");
                    Console.WriteLine();
                    authorBooks.ForEach(authBook =>
                    {
                        Console.WriteLine($"{authBook.Title} skriven år {authBook.Publish}");
                        if (authBook.Reviews.Count > 0)
                        {
                            Console.WriteLine($"Betyg: {authBook.Reviews.Average()}");
                        }
                        Console.WriteLine();
                    });
                }

                Console.WriteLine();
            });
        }

        private static string CleanUp(string inputString)
        {
            //Vi sätter input strängen till lowercase så att vi kan börja modifiera ordet.
            inputString = inputString.ToLower();
            //Vi delar input strängen till ord, kapitaliserar första bokstaven av varje ord, och sedan sätter tillbaka dom till en sträng.
            return string.Join(" ", inputString.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1)));
        }
    }
}
