class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int PageLength { get; set; }
    public int YearPublished { get; set; }
    public bool IsCheckedOut { get; set; }

    public Book(string title, string author, string genre, int pages, int year, bool isCheckedOut)
    {
        Title = title;
        Author = author;
        Genre = genre;
        PageLength = pages;
        YearPublished = year;
        IsCheckedOut = isCheckedOut;
    }

    public override string ToString()
    {
        string status = IsCheckedOut ? "Checked Out" : "Available";
        return $"{Title} by {Author} | {Genre} | {PageLength} pages | {YearPublished} | {status}";
    }
}
