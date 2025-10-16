using System;
using System.IO;
using System.Collections.Generic;

class Test
{
    static void Main(string[] args)
    {
        Library lib = new Library();

        string path = "library-books.csv";

        // Check if file exists
        if (File.Exists(path))
        {
            Console.WriteLine("File Exists\n");

            // Load the books from the CSV file
            string[] lines = File.ReadAllLines(path);

            // Skip the header line (first line)
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                if (parts.Length == 6)
                {
                    string title = parts[0].Trim();
                    string author = parts[1].Trim();
                    string genre = parts[2].Trim();
                    int pages = int.Parse(parts[3].Trim());
                    int year = int.Parse(parts[4].Trim());
                    bool isCheckedOut = bool.Parse(parts[5].Trim());

                    Book b = new Book(title, author, genre, pages, year, isCheckedOut);
                    lib.AddBook(b);
                }
            }

            Console.WriteLine("Library data loaded successfully!");
        }
        else
        {
            Console.WriteLine("File does not exist");
            return; // stop program if file can’t be found
        }

        // Main menu loop
        while (true)
        {
            Console.WriteLine("\n===== Library Menu =====");
            Console.WriteLine("1. View All Books");
            Console.WriteLine("2. View Available Books");
            Console.WriteLine("3. Search by Author");
            Console.WriteLine("4. Filter by Year");
            Console.WriteLine("5. Sort by Page Length");
            Console.WriteLine("6. Search by Title");
            Console.WriteLine("7. Check Out a Book");
            Console.WriteLine("8. Return a Book");
            Console.WriteLine("9. Sort by Title (A–Z)");
            Console.WriteLine("0. Quit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": // view all books
                    Console.WriteLine("-- Viewing All Books --\n");
                    foreach (Book b in lib.GetAllBooks())
                        Console.WriteLine(b);
                    break;

                case "2": // view available books
                    Console.WriteLine("-- Viewing Available Books --\n");
                    foreach (Book b in lib.GetAvailableBooks())
                        Console.WriteLine(b);
                    break;

                case "3": // search by author
                    Console.Write("Enter Author name: ");
                    string author = Console.ReadLine();
                    Console.WriteLine();
                    foreach (Book b in lib.GetBooksByAuthor(author))
                        Console.WriteLine(b);
                    break;

                case "4": // filter by year
                    Console.Write("Enter year: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    foreach (Book b in lib.GetBooksAfterYear(year))
                        Console.WriteLine(b);
                    break;

                case "5": // sort by page length (Stretch Goal #6)
                    Console.WriteLine("-- Sorting by Page Length --\n");
                    lib.SortByPageLength();
                    foreach (Book b in lib.GetAllBooks())
                        Console.WriteLine(b);
                    break;

                case "6": // search by title
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();
                    Console.WriteLine();
                    Book found = lib.SearchByTitle(title);
                    if (found != null)
                        Console.WriteLine(found);
                    else
                        Console.WriteLine("Book not found.");
                    break;

                case "7": // check out (Stretch Goal #5)
                    Console.Write("Enter title to check out: ");
                    string checkoutTitle = Console.ReadLine();
                    Console.WriteLine();
                    if (lib.CheckOutBook(checkoutTitle))
                        Console.WriteLine($"Book checked out! Due back in 14 days: {DateTime.Now.AddDays(14):d}");
                    else
                        Console.WriteLine("That book is already checked out or doesn't exist.");
                    break;

                case "8": // return
                    Console.Write("Enter title to return: ");
                    string returnTitle = Console.ReadLine();
                    Console.WriteLine();
                    if (lib.ReturnBook(returnTitle))
                        Console.WriteLine("Book returned successfully!");
                    else
                        Console.WriteLine("That book wasn’t checked out or doesn’t exist.");
                    break;

                case "9": // sort by title (Stretch Goal #1)
                    Console.WriteLine("-- Sorting by Title (A–Z) --\n");
                    foreach (Book b in lib.GetBooksSortedByTitle())
                        Console.WriteLine(b);
                    break;

                case "0":
                    Console.WriteLine("-- Exiting Program --");
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
