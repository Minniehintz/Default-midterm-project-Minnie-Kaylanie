using System;
using System.IO;
using System.Collections.Generic;

class Test
{
    static void Main(string[] args)
    {
        Library lib = new Library();

        string path = "books.csv";

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
            return; // stops program if the file can’t be found
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

                    List<Book> authorResults = lib.GetBooksByAuthor(author);

                    if (authorResults.Count == 0)
                        Console.WriteLine("The Library doesn't have any books by that author");
                    else
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
                    string titleSearch = Console.ReadLine();
                    Console.WriteLine();

                    List<Book> titleResults = lib.GetBooksByTitle(titleSearch);

                    if (titleResults.Count == 0)
                        Console.WriteLine("The Library doesn't have any books with that title");
                    else
                        foreach (Book b in titleResults)
                            Console.WriteLine(b);
                    break;

                case "7": // check out (Stretch Goal 5)
                    {
                        while (true)
                        {
                            Console.Write("Enter part of the title to check out: ");
                            string checkoutTitle = Console.ReadLine();
                            Console.WriteLine();

                            List<Book> checkoutMatches = lib.GetBooksByTitle(checkoutTitle);

                            if (checkoutMatches.Count == 0)
                            {
                                Console.WriteLine("No matching books found. Try again.");
                                continue;
                            }

                            Book bookToCheckout = checkoutMatches[0];

                            if (bookToCheckout.IsCheckedOut)
                            {
                                Console.WriteLine("That book is already checked out. Try a different title.");
                                continue;
                            }

                            bookToCheckout.IsCheckedOut = true;
                            Console.WriteLine($"Book checked out! Due back in 14 days: {DateTime.Now.AddDays(14):d}");
                            break;
                        }

                        break;
                    }


                case "8": // return
                    {
                        while (true) 
                        {
                            Console.Write("Enter part of the title to return: ");
                            string returnTitle = Console.ReadLine();
                            Console.WriteLine();

                            List<Book> returnMatches = lib.GetBooksByTitle(returnTitle);

                            if (returnMatches.Count == 0)
                            {
                                Console.WriteLine("No matching books found. Try again.");
                                continue;
                            }

                            Book bookToReturn = returnMatches[0];

                            if (!bookToReturn.IsCheckedOut)
                            {
                                Console.WriteLine("That book is not currently checked out. Try a different title.");
                                continue;
                            }

                            bookToReturn.IsCheckedOut = false;
                            Console.WriteLine("Book returned successfully!");
                            break; 
                        }
                        break; 
                    }

                case "9": // sort by title (Stretch Goal #1)
                    Console.WriteLine("-- Sorting by Title (A–Z) --\n");
                    foreach (Book b in lib.GetBooksSortedByTitle())
                        Console.WriteLine(b);
                    break;

                case "0":
                    Console.WriteLine("-- Exiting Program --");
                    Environment.Exit(0);
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
