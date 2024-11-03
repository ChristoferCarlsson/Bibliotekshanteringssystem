using librarysystem;
using Librarysystem;
using System.Text.Json;
using System.Xml.Linq;

namespace Librarysystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string title;
            string author;
            string name;

            //Vi Välkommnar och skapar biblioteket
            Console.WriteLine("Välkommen!");
            Library library = new Library();

            //Vi skapar en program loop
            bool running = true;
            while (running)
            {
                PrintList();
                string input = Console.ReadLine();

                switch (input)
                {

                    case "1":
                        //Vi skapar en bok
                        Console.WriteLine("Vad heter boken?");
                        title = Console.ReadLine();
                        if (string.IsNullOrEmpty(title)) 
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        Console.WriteLine("Vad heter författaren?");
                        author = Console.ReadLine();
                        if (string.IsNullOrEmpty(author))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        List<string> genreList = MultipleChoiceLoop("Vad har den för genrar? Avsluta genom att skriva 'klar'");
                        if (string.IsNullOrEmpty(genreList[0]))
                        {
                            Console.WriteLine("Var vänlig och välj åtminstånde en genre!");
                            break;
                        }

                        genreList.ForEach(gen => Console.WriteLine(gen));

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
                            break;
                        }

                        Console.WriteLine("Vad har den för ISBN?");
                        inp = Console.ReadLine();
                        int isbn;
                        try
                        {
                            isbn = Int32.Parse(inp);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"{inp} är inget nummer!");
                            break;
                        }

                        List<string> reviewList = MultipleChoiceLoop("Vad har den för reviews? Avsluta eller skippa genom att skriva 'klar'");


                        library.CreateBook(title, author, genreList, publish, isbn, reviewList);

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();

                        break;


                    case "2":
                        Console.WriteLine("Vad heter författaren?");
                        name = Console.ReadLine();
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        Console.WriteLine("Skriv en beskrivning av författaren.");
                        string description = Console.ReadLine();
                        if (string.IsNullOrEmpty(description))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }
                        library.CreateAuthor(name, description);
                        break;


                    case "3":
                        ListSearch(library);
                        break;

                    case "4":

                        library.PrintAuthors();
                        library.PrintBooks();
                        Console.WriteLine();
                        break;

                    case "5":
                        break;

                    case "6":
                        ListHandle(library);
                        break;

                    case "7":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Jag förstår inte");
                        break;

                }
            }

        }

        //Listan som visas i början av programmet
        static void PrintList()
        {
            Console.WriteLine("1. Lägg till en bok");
            Console.WriteLine("2. Lägg till en författare");
            Console.WriteLine("3. Sök och filtrera böcker");
            Console.WriteLine("4. Lista alla böcker och författare");
            Console.WriteLine("5. Ge betyg");
            Console.WriteLine("6. Hantera bok och författare");
            Console.WriteLine("7. Avsluta");
        }

        static void PrintListSearch()
        {
            Console.WriteLine("1. Sök efter bok titel");
            Console.WriteLine("2. Sök efter författare");
            Console.WriteLine("3. Filtrera efter genre");
            Console.WriteLine("4. Sortera efter författare");
            Console.WriteLine("5. Sortera efter publiceringsår");
            Console.WriteLine("6. Tillbaka");
        }

        static void PrintListHandle()
        {
            Console.WriteLine("1. Ta bort bok");
            Console.WriteLine("2. Ta bort författare");
            Console.WriteLine("3. Uppdatera bok");
            Console.WriteLine("4. Uppdatera författare");
            Console.WriteLine("5. Tillbaka");
        }

        static List<string> MultipleChoiceLoop(string text)
        {
            bool running = true;
            List<string> genreList = new List<string>();
            while (running)
            {
                Console.WriteLine(text);
                string genreInput = Console.ReadLine().ToLower();

                if (genreInput == "klar")
                {
                    running = false;
                    break;
                }
                else
                {
                    genreList.Add(genreInput);
                }
            }

            return genreList.ToList();
        }

        static void ListSearch(Library library)
        {
            string name;
            string title;

            bool running = true;
            while (running)
            {
                PrintListSearch();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Vad heter boken som du vill söka efter?");
                        title = Console.ReadLine();
                        library.SearchBook("Title", title);
                        break;

                    case "2":
                        Console.WriteLine("Vad heter författaren som du vill söka efter?");
                        name = Console.ReadLine();
                        library.SearchBook("Author",name);
                        break;

                    case "3":
                        Console.WriteLine("Vilken genre vill du söka efter?");
                        string genre = Console.ReadLine();
                        library.FilterGenre(genre);
                        break;

                    case "4":
                        library.Sort("author");
                        break;

                    case "5":
                        library.Sort("publish");
                        break;

                    case "6":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Jag förstår inte.");
                        break;
                }

            }
        }

        static void ListHandle(Library library)
        {
            string title;

            bool running = true;
            while (running)
            {
                PrintListHandle();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Vad heter boken som du vill ta bort?");
                         title = Console.ReadLine();
                        if (string.IsNullOrEmpty(title))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        library.RemoveBook(title);
                        break;

                    case "2":
                        Console.WriteLine("Vad heter boken som du vill ta bort?");
                        title = Console.ReadLine();
                        if (string.IsNullOrEmpty(title))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        library.RemoveBook(title);
                        break;

                    case "3":
                        Console.WriteLine("Vad heter boken som du vill ändra?");
                        title = Console.ReadLine();
                        if (string.IsNullOrEmpty(title))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        library.UpdateBook(title);
                        break;

                    case "4":
                        break;

                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Jag förstår inte.");
                        break;
                }

            }
        }
    }
}








//                    case "2":
//    library.PrintBooks();
//    break;

//case "3":
//    library.PrintAuthors();
//    break;

//case "4":
//    Console.WriteLine("Vad heter författaren?");
//    name = Console.ReadLine();
//    if (string.IsNullOrEmpty(name))
//    {
//        Console.WriteLine("Var vänlig och fyll i alla fält!");
//        break;
//    }

//    Console.WriteLine("Skriv en beskrivning av författaren.");
//    string description = Console.ReadLine();
//    if (string.IsNullOrEmpty(description))
//    {
//        Console.WriteLine("Var vänlig och fyll i alla fält!");
//        break;
//    }
//    library.CreateAuthor(name, description);
//    break;

//case "5":
//Console.WriteLine("Vad heter boken som du vill ta bort?");
//title = Console.ReadLine();
//if (string.IsNullOrEmpty(title))
//{
//    Console.WriteLine("Var vänlig och fyll i alla fält!");
//    break;
//}

//library.RemoveBook(title);
//break;

//case "6":
//    Console.WriteLine("Vad heter författaren som du vill ta bort?");
//    name = Console.ReadLine();
//    if (string.IsNullOrEmpty(name))
//    {
//        Console.WriteLine("Var vänlig och fyll i alla fält!");
//        break;
//    }

//    library.RemoveAuthor(name);
//    break;



//case "7":
//    break;

//case "8":
//    break;

//case "9":
//    break;

//case "10":
//    break;

//case "12":
//    running = false;
//    break;

//default:
//    Console.WriteLine("Jag förstår inte");
//    break;






















//string dataJSONfilPath = "LibraryData.json";
//string allaDataSomJSONType = File.ReadAllText(dataJSONfilPath);

//MyDatabase myDatabase = JsonSerializer.Deserialize<MyDatabase>(allaDataSomJSONType)!;
//List<Book> allBooks = myDatabase.allBooksFromDB;


//List<Book> allBooksAfter2002 = allBooks.Where(book => book.Publish > 2002).ToList();
//List<Book> allBooksAfter2002InOrder = allBooksAfter2002.OrderBy(book => book.Publish).ToList();
//List<Book> allBooksAfter2002ReverseOrder = allBooksAfter2002.OrderByDescending(book =>  book.Publish).ToList();

//allBooksAfter2002InOrder.ForEach(book => Console.WriteLine(book.Title));

//Console.WriteLine(allBooks.Count);

////allBooks.Add(new Book(allBooks.Count, "Test Bok 3", "Johan Bengtsson", ["Comedy", "Romance"], 2009, 12335, false, [2, 2, 1, 3, 2]));

////List<Student> allaStudenter = myDatabase.AllStudenterFrånDB;

////var grupperadeStudenterEnligtBetyg = allaStudenter.GroupBy(student => student.Betyg).Select(group => new { Betyg = group.Key, Count = group.Count() }).ToList();

////grupperadeStudenterEnligtBetyg.ForEach(group => Console.WriteLine($"Betyg: {group.Betyg} / Antal studenter : {group.Count}"));

////allaStudenter.Add(new Student("Balen", 10));


//string updatedJSON = JsonSerializer.Serialize(myDatabase, new JsonSerializerOptions { WriteIndented = true });

//File.WriteAllText(dataJSONfilPath, updatedJSON);