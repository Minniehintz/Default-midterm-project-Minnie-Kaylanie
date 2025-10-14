class Test
{
    static void Main(string[] args)
    {
        Library lib = new Library();

        // TODO: Load books here (file reading)

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

        }
    }
}
