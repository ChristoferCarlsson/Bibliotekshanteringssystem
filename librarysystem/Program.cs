using librarysystem;

namespace Librarysystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Variablar som vi återanvänder mycket
            string title;
            string author;
            string name;
            bool notCorrectValue;

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
                        //Om inputen är tom eller null
                        if (CheckIfEmpty(title)) break;

                        Console.WriteLine("Vad heter författaren?");
                        author = Console.ReadLine();
                        if (CheckIfEmpty(author)) break;

                        //Vi kollar så att åtminstånde ett genre har blivit vald, och att den är korrekt ifylld
                        List<string> genreList = AddGenres("Vad har den för genrar? Avsluta genom att skriva 'klar'");

                        Console.WriteLine("Vilket år var den publiserad?");
                        string inp = Console.ReadLine();
                        //Vi kollar om inputen kan bli ett nummer
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

                        library.CreateBook(title, author, genreList, publish, isbn);

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;

                    case "2":
                        //Vi skapar författaren
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

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
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
                        Console.WriteLine("Vilken bok vill du ge betyg till?");
                        title = Console.ReadLine();
                        if (string.IsNullOrEmpty(title))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        Console.WriteLine("Vilket betyg vill du ge den?");
                        Console.WriteLine("Mellan 1 till 5");
                        inp = Console.ReadLine();
                        //Vi kollar om inputen kan bli ett nummer
                        try
                        {
                            publish = Int32.Parse(inp);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"{inp} är inget nummer!");
                            break;
                        }

                        //Vi kollar så att värdet är mellan 1 och 5.
                        if (publish > 5 || publish < 0)
                        {
                            Console.WriteLine("Sätt ett värde mellan 1 till 5");
                            break;
                        }

                        library.SetReview(title, publish);

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;

                    case "6":
                        ListHandle(library);
                        break;

                    case "7":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Jag förstår inte");
                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
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
                        library.SearchBook("title", title);
                        break;

                    case "2":
                        Console.WriteLine("Vad heter författaren som du vill söka efter?");
                        name = Console.ReadLine();
                        library.SearchBook("author", name);
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
                        Console.Clear();
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
            string name;

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

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;

                    case "2":
                        Console.WriteLine("Vad heter författaren som du vill ta bort?");
                        title = Console.ReadLine();
                        if (string.IsNullOrEmpty(title))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        library.RemoveAuthor(title);

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
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

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;

                    case "4":
                        Console.WriteLine("Vad heter författaren som du vill ändra?");
                        name = Console.ReadLine();
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Var vänlig och fyll i alla fält!");
                            break;
                        }

                        library.UpdateAuthor(name);

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;

                    case "5":
                        running = false;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Jag förstår inte.");

                        //Ge lite väntetid för effekt
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }

            }
        }

        static List<string> AddGenres(string text)
        {
            bool running = true;
            List<string> genreList = new List<string>();
            while (running)
            {
                Console.WriteLine(text);
                string genreInput = Console.ReadLine().ToLower();

                if (genreInput == "")
                {
                    Console.WriteLine("Var vänlig och fyll i genre");
                }
                else
                {
                    if (genreInput == "klar")
                    {
                        if (genreList.Count == 0)
                        {
                            Console.WriteLine("Var vänlig och välj åtminstånde en genre!");
                        }
                        else
                        {
                            running = false;
                            break;
                        }
                    }
                    else
                    {
                        genreList.Add(genreInput);
                    }
                }
            }

            return genreList.ToList();
        }

        private static bool CheckIfEmpty(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Var vänlig och fyll i alla fält!");
                return true;
            }
            return false;
        }

    }
}