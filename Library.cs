using System;
using System.Collections.Generic;
using System.Linq; // Needed for OrderBy()

class Library
{
    private List<Book> books = new List<Book>();

    // Add a book to the list
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
        List<Book> availableBooks = new List<Book>();

        foreach (Book b in books)
        {
            if (!b.IsCheckedOut)
            {
                availableBooks.Add(b);
            }
        }

        return availableBooks;
    }

    // Return books by a given author
    public List<Book> GetBooksByAuthor(string author)
    {
        List<Book> result = new List<Book>();
        string search = author.ToLower();

        foreach (Book b in books)
        {
            if (b.Author.ToLower().Contains(search))
            {
                result.Add(b);
            }
        }

        return result;
    }

    // Return books published on or after a given year
    public List<Book> GetBooksAfterYear(int year)
    {
        List<Book> result = new List<Book>();

        foreach (Book b in books)
        {
            if (b.YearPublished >= year)
            {
                result.Add(b);
            }
        }

        return result;
    }

    // Sort books by page length (Stretch Goal #6 — faster built-in sort)
    public void SortByPageLength()
    {
        books = books.OrderBy(b => b.PageLength).ToList();
    }

    // Sort books alphabetically by title (Stretch Goal #1)
    public List<Book> GetBooksSortedByTitle()
    {
        return books.OrderBy(b => b.Title).ToList();
    }

    // Find a book by its title
    public List <Book> GetBooksByTitle(string title)
    {
        List <Book> result = new List<Book>();  
        string search = title.ToLower();

        foreach (Book b in books)
        {
            if (b.Title.ToLower().Contains(search))
            {
                result.Add(b);
            }
        }

        return result;
    }

    public Book SearchByTitle(string title)
    {
        string search = title.ToLower();
        foreach (Book b in books)
        {
            if (b.Title.ToLower().Contains(search))
            {
                return b; 
            }
        }
        return null;
    }


    // Check out a book
    public bool CheckOutBook(string title)
    {
        Book b = SearchByTitle(title);

        if (b != null && !b.IsCheckedOut)
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

        if (b != null && b.IsCheckedOut)
        {
            b.IsCheckedOut = false;
            return true;
        }

        return false;
    }
}
