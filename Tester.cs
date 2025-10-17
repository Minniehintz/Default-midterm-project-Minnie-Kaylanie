using System;
using System.IO;
using System.Collections.Generic;

class Test
{
    static void Main(string[] args)
    {
        Library lib = new Library();
        string path = "books.csv";

        // ---- Load CSV safely ----
        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist: " + path);
                return;
            }

            Console.WriteLine("File Exists\n");

            string[] lines = File.ReadAllLines(path);

            if (lines.Length <= 1)
            {
                Console.WriteLine("CSV has no data rows (only header).");
                return;
            }

            // Skip header and read rows
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(',');

                if (parts.Length < 6)
                {
                    Console.WriteLine($"Skipping line {i + 1}: expected 6 columns, got {parts.Length}");
                    continue;
                }

                string title = parts[0].Trim();
                string author = parts[1].Trim();
                string genre = parts[2].Trim();

                if (!int.TryParse(parts[3].Trim(), out int pages))
                {
                    Console.WriteLine($"Skipping line {i + 1}: PageLength is not a number.");
                    continue;
                }

                if (!int.TryParse(parts[4].Trim(), out int year))
                {
                    Console.WriteLine($"Skipping line {i + 1}: YearPublished is not a number.");
                    continue;
                }

                if (!bool.TryParse(parts[5].Trim(), out bool isCheckedOut))
                {
                    Console.WriteLine($"Skipping line {i + 1}: IsCheckedOut must be true/false.");
                    continue;
                }

                lib.AddBook(new Book(title, author, genre, pages, year, isCheckedOut));
            }

            Console.WriteLine("Library data loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading CSV: " + ex.Message);
            return;
        }

        // ---- Main Menu Loop ----
        while (true)
        {
            Console.WriteLine("\n===== Library Menu =====");
            Console.WriteLine("1. View All Books");
            Console.WriteLine("2. View Available Books");
            Console.WriteLine("3. Search by Author (partial ok)");
            Console.WriteLine("4. Filter by Year (on/after)");
            Console.WriteLine("5. Sort by Page Length");
            Console.WriteLine("6. Search by Title (partial ok)");
            Console.WriteLine("7. Check Out a Book");
            Console.WriteLine("8. Return a Book");
            Console.WriteLine("9. Sort by Title (A–Z)");
            Console.WriteLine("0. Quit");
            Console.Write("Enter choice: ");

            string choice = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("-- Viewing All Books --\n");
                        PrintList(lib.GetAllBooks());
                        break;

                    case "2":
                        Console.WriteLine("-- Viewing Available Books --\n");
                        PrintList(lib.GetAvailableBooks());
                        break;

                    case "3":
                        Console.Write("Enter part of the author name: ");
                        {
                            string author = (Console.ReadLine() ?? "").Trim();
                            if (author.Length == 0) { Console.WriteLine("Author cannot be empty."); break; }

                            List<Book> authorResults = lib.GetBooksByAuthor(author);
                            if (authorResults.Count == 0)
                                Console.WriteLine("No books by that author.");
                            else
                                PrintList(authorResults);
                        }
                        break;

                    case "4":
                        Console.Write("Enter minimum year: ");
                        {
                            string rawYear = Console.ReadLine() ?? "";
                            if (int.TryParse(rawYear, out int year))
                                PrintList(lib.GetBooksAfterYear(year));
                            else
                                Console.WriteLine("Invalid year — please enter a number.");
                        }
                        break;

                    case "5":
                        Console.WriteLine("-- Sorting by Page Length --\n");
                        lib.SortByPageLength();
                        PrintList(lib.GetAllBooks());
                        break;

                    case "6":
                        Console.Write("Enter part of the book title: ");
                        {
                            string titleSearch = (Console.ReadLine() ?? "").Trim();
                            if (titleSearch.Length == 0) { Console.WriteLine("Title cannot be empty."); break; }

                            List<Book> titleResults = lib.GetBooksByTitle(titleSearch);
                            if (titleResults.Count == 0)
                                Console.WriteLine("No books with that title.");
                            else
                                PrintList(titleResults);
                        }
                        break;

                    case "7":
                        while (true)
                        {
                            Console.Write("Enter part of the title to check out: ");
                            string checkoutTitle = (Console.ReadLine() ?? "").Trim();
                            if (checkoutTitle.Length == 0) { Console.WriteLine("Title cannot be empty."); continue; }

                            List<Book> checkoutMatches = lib.GetBooksByTitle(checkoutTitle);
                            if (checkoutMatches.Count == 0) { Console.WriteLine("No matching books found. Try again."); continue; }

                            Book bookToCheckout = checkoutMatches[0];
                            if (bookToCheckout.IsCheckedOut) { Console.WriteLine("That book is already checked out. Try a different title."); continue; }

                            bookToCheckout.IsCheckedOut = true;
                            Console.WriteLine($"Book checked out! Due back in 14 days: {DateTime.Now.AddDays(14):d}");
                            break;
                        }
                        break;

                    case "8":
                        while (true)
                        {
                            Console.Write("Enter part of the title to return: ");
                            string returnTitle = (Console.ReadLine() ?? "").Trim();
                            if (returnTitle.Length == 0) { Console.WriteLine("Title cannot be empty."); continue; }

                            List<Book> returnMatches = lib.GetBooksByTitle(returnTitle);
                            if (returnMatches.Count == 0) { Console.WriteLine("No matching books found. Try again."); continue; }

                            Book bookToReturn = returnMatches[0];
                            if (!bookToReturn.IsCheckedOut) { Console.WriteLine("That book is not currently checked out. Try a different title."); continue; }

                            bookToReturn.IsCheckedOut = false;
                            Console.WriteLine("Book returned successfully!");
                            break;
                        }
                        break;

                    case "9":
                        Console.WriteLine("-- Sorting by Title (A–Z) --\n");
                        PrintList(lib.GetBooksSortedByTitle());
                        break;

                    case "0":
                    case "q":
                        Console.WriteLine("-- Exiting Program --");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    private static void PrintList(List<Book> books)
    {
        if (books == null || books.Count == 0)
        {
            Console.WriteLine("(no results)");
            return;
        }
        foreach (var b in books) Console.WriteLine(b);
    }
}
