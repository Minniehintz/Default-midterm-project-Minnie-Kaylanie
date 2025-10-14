class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book b)
    {
        books.Add(b);
    }

    public List<Book> GetAllBooks() => books;

    public List<Book> GetAvailableBooks() =>
        books.Where(b => !b.IsCheckedOut).ToList();

    public List<Book> GetBooksByAuthor(string author) =>
        books.Where(b => b.Author.ToLower() == author.ToLower()).ToList();

    public List<Book> GetBooksAfterYear(int year) =>
        books.Where(b => b.YearPublished >= year).ToList();

    public void SortByPageLength() =>
        books = books.OrderBy(b => b.PageLength).ToList();

    public Book SearchByTitle(string title) =>
        books.FirstOrDefault(b => b.Title.ToLower() == title.ToLower());

    public bool CheckOutBook(string title)
    {
        var book = SearchByTitle(title);
        if (book != null && !book.IsCheckedOut)
        {
            book.IsCheckedOut = true;
            return true;
        }
        return false;
    }

    public bool ReturnBook(string title)
    {
        var book = SearchByTitle(title);
        if (book != null && book.IsCheckedOut)
        {
            book.IsCheckedOut = false;
            return true;
        }
        return false;
    }
}
