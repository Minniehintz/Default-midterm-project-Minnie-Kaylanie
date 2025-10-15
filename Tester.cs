using System.IO;
class Test
{
    static void Main(string[] args)
    {
        Library lib = new Library();

        string path = "library-books.csv";

        if (File.Exists(path))
        {
            Console.WriteLine("File Exists");
        }
        else
        {
            Console.WriteLine("File does not exist");
        }

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
                Console.WriteLine("9. Quit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();



            switch (choice)
            {
                case "1":
                    Console.WriteLine("-- Viewing All Books --");
                    Console.WriteLine();
                    break;

                case "2":
                    Console.WriteLine("-- Viewing Available Books --");
                    Console.WriteLine();
                    break;

                case "3":
                    Console.Write("Enter Author name: ");
                    string author = Console.ReadLine();
                    Console.WriteLine();
                    break;

                case "4":
                    Console.Write("Enter year: ");
                    Console.WriteLine();
                    break;

                case "5":
                    Console.WriteLine("-- Sorting by page length --");
                    Console.WriteLine();
                    break;

                case "6":
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();
                    Console.WriteLine();
                    break;

                case "7":
                    Console.Write("Enter title to check out: ");
                    string checkoutTitle = Console.ReadLine();
                    Console.WriteLine();
                    break;

                case "8":
                    Console.Write("Enter title to return: ");
                    string returnTitle = Console.ReadLine();
                    Console.WriteLine();
                    break;

                case "9":
                    Console.WriteLine("-- Exiting Program --");
                    break;
            }
            
        }   

    }
}
