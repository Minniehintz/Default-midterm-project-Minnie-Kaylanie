class Library
{
    private List<Book> books = new List<Book>();

    // Add a book to list
    public void AddBook(Book b) 
    {
        books.Add(b);
    }

    // Return all books
    public List<Book> GetAllBooks() 
    {
        return books;
    }

    // Return only available books
    public List<Book> GetAvailableBooks()
    {
        List<Book> availableBook = new List<Book>();

        foreach (Book b in books)
        {
            if (b.IsCheckedOut == false)
            {
                availableBook.Add(b);
            }
        }
        return availableBook;
    }

    // Return books by author
    public List<Book> GetBooksByAuthor(string author) 
    {
        List<Book> result = new List<Book>();
        foreach (Book b in books)
        {
            if(b.Author.Equals(author))
            {
                result.Add(b); 
            }
        }
        return result;  
    }

    // Filter by year
    public List<Book> GetBooksAfterYear(int year) 
    { 
        List <Book> result = new List<Book>();

        foreach (Book b in books)
        {
            if (b.YearPublished >= year)
            {
                result.Add(b);
            }
        }
        return result;
    }

    // Sort by page length
    public void SortByPageLength() 
    {
        for (int i = 0; i < books.Count - 1; i++)
        {
            for (int j = 0; j < books.Count; j++)
            {
                if (books[j].PageLength > books[j + 1].PageLength)
                {
                    Book temp = books[j];
                    books[j] = books[j + 1];
                    books[j + 1] = temp;
                }
            }
        }
    }

    // Find specific book by title
    public Book SearchByTitle(string title) 
    {
        foreach (Book b in books)
        {
            if (b.Title.Equals(title))
            {
                return b;
            }
        }
        return null;
    }

    // Check out a book
    public bool CheckOutBook(string title) 
    {
        Book b= SearchByTitle(title);

        if (b!=null && b.IsCheckedOut == false)
        {
            b.IsCheckedOut = true;
            return true;
        }
        return false;
    }

    // Return a book
    public bool ReturnBook(string title) 
    {
        Book b = SearchByTitle(title);

        if (b!= null && b.IsCheckedOut == true)
        {
            b.IsCheckedOut = false;
            return true;
        }
        return false;
    }
}
