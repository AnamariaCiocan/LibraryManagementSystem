using LibraryManagementSystem.model;
using LibraryManagementSystem.repository;

namespace LibraryManagementSystem.service;

public class BookService
{
    private IBookRepository bookRepo;

    public BookService(BookRepository books)
    {
        this.bookRepo = books;
    }

    public void addNewBook(string title, string author,string genre, int quantity)
    {
        bookRepo.save(new Book(title, author, genre, quantity));
    }

    public void updateBook(int id, string title, string author,string genre, int quantity)
    {
        getBookById(id);
        Book book = new Book(title, author,genre, quantity);
        book.setId(id);
        bookRepo.update(id, book);
    }

    public void deleteBook(string name, string author)
    {
        Book book = getBookByTitleAndAuthor(name, author);
        if (book == null)
        {
            throw new Exception("Book not found");
        }
        bookRepo.delete(book.getId());
    }

    public Book getBookById(int id)
    {
        Book book = bookRepo.findOne(id);
        if (book == null)
        {
            throw new Exception("Id not found");
        }
        return book;
    }

    public IList<Book> getAllBooks()
    {
        IList<Book> books = bookRepo.getAll();
        if (books.Count == 0)
        {
            throw new Exception("No books found");
        }
        return books;
    }

    public IList<Book> getAllBooksByAuthor(string author)
    {
        IList<Book> books = bookRepo.findByAuthor(author);
        if (books == null)
        {
            throw new Exception("Author not found");
        }
        return books;
    }

    public IList<Book> getAllBooksByTitle(string title)
    {
        IList<Book> books = bookRepo.findByTitle(title);
        if (books == null)
        {
            throw new Exception("Title not found");
        }
        return bookRepo.findByTitle(title);
    }

    public int checkQuantity(string title, string author)
    {
        Book book = bookRepo.findOneByTitleAndAuthor(title, author);
        return book.getCurrentQuantity();
    }

    public Book getBookByTitleAndAuthor(string title, string author)
    {
        Book book = bookRepo.findOneByTitleAndAuthor(title, author);
        if (book == null)
        {
            throw new Exception("Book not found");
        }
        return book;
    }
    
    public IList<Book> getAllBooksByGenre(string genre)
    {
        IList<Book> books = bookRepo.findByGenre(genre);
        if (books == null)
        {
            throw new Exception("Genre not found");
        }
        return books;
    }

    public Book getRandomBookRecomandationByGenre(string genre)
    {
        IList<Book> books = getAllBooksByGenre(genre);
        Random random = new Random();
        int randomNumber = random.Next(books.Count);
        Book book = books[randomNumber];
        return book;
    }
    
}