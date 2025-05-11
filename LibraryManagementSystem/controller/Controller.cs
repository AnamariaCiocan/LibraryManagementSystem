using LibraryManagementSystem.model;
using LibraryManagementSystem.service;
using LibraryManagementSystem.validation;

namespace LibraryManagementSystem.controller;

public class Controller
{
    private BookService bookService;
    private PersonService personService;
    private BorrowService borrowService;
    private ReviewService reviewService;
    public Controller(BookService bs, PersonService ps, BorrowService brs, ReviewService rs)
    {
        bookService = bs;
        personService = ps;
        borrowService = brs;
        reviewService = rs;
    }

    public void addNewBook()
    {
        try
        {
            Console.WriteLine("Title of the book: ");
            string bookTitle = Console.ReadLine();
            Validator.checkEmptyString(bookTitle);
            Console.WriteLine("Author of the book: ");
            string bookAuthor = Console.ReadLine();
            Validator.checkEmptyString(bookAuthor);
            Console.WriteLine("Genre of the book: ");
            string bookGenre = Console.ReadLine();
            Validator.checkEmptyString(bookGenre);
            Console.WriteLine("Available(quantity): ");
            int bookAvailable = int.Parse(Console.ReadLine());
            Validator.checkNumber(bookAvailable.ToString());
            bookService.addNewBook(bookTitle, bookAuthor, bookGenre, bookAvailable);
            Console.WriteLine("Book added successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }

    public void removeBook()
    {
        
        try
        {
            Console.WriteLine("Title of the book: ");
            string bookTitle = Console.ReadLine();
            Validator.checkEmptyString(bookTitle);
            Console.WriteLine("Author of the book: ");
            string bookAuthor = Console.ReadLine();
            Validator.checkEmptyString(bookAuthor);
            bookService.deleteBook(bookTitle, bookAuthor);
            Console.WriteLine("Booked removed successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void searchBooksByTitle()
    {
        try
        { 
             Console.WriteLine("Title of the book: ");
             string bookTitle = Console.ReadLine();
             Validator.checkEmptyString(bookTitle);
             listBooks(bookService.getAllBooksByTitle(bookTitle));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void searchBooksByAuthor()
    {
       
        try
        {
            Console.WriteLine("Author of the book: ");
            string bookAuthor = Console.ReadLine();
            Validator.checkEmptyString(bookAuthor);
            listBooks(bookService.getAllBooksByAuthor(bookAuthor));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void searchBooksByGenre()
    {
      
        try
        {
            Console.WriteLine("Genre of the book: ");
            string bookGenre = Console.ReadLine();
            Validator.checkEmptyString(bookGenre);
            listBooks(bookService.getAllBooksByGenre(bookGenre));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void searchBooksByTitleAndAuthor()
    {
       
        try
        { 
             Console.WriteLine("Title of the book: ");
             string bookTitle = Console.ReadLine();
             Validator.checkEmptyString(bookTitle);
             Console.WriteLine("Author of the book: ");
             string bookAuthor = Console.ReadLine();
             Validator.checkEmptyString(bookAuthor);
             Book book = bookService.getBookByTitleAndAuthor(bookTitle, bookAuthor);
             Console.WriteLine(book.toString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    

    public void addNewBorrowing()
    {
       
        try
        {  
            Console.WriteLine("Date of borrow(yyyy-mm-dd): ");
            string borrowDateStr = Console.ReadLine();
            Validator.checkDate(borrowDateStr);
            DateTime borrowDate = DateTime.Parse(borrowDateStr);
            Console.WriteLine("Due date of borrowing(yyyy-mm-dd): ");
            string dueDateStr = Console.ReadLine();
            Validator.checkDate(dueDateStr);
            DateTime dueDate = DateTime.Parse(dueDateStr);
            Console.WriteLine("Book title: ");
            string bookTitle = Console.ReadLine();
            Validator.checkEmptyString(bookTitle);
            Console.WriteLine("Book author: ");
            string bookAuthor = Console.ReadLine();
            Validator.checkEmptyString(bookAuthor);
            Console.WriteLine("Person's name: ");
            string personName = Console.ReadLine();
            Validator.checkEmptyString(personName);
            Console.WriteLine("Person's phone number: ");
            string personPhone = Console.ReadLine();
            Validator.checkPhoneNumber(personPhone);
            borrowService.saveBorrow(borrowDate, dueDate, bookTitle, bookAuthor, personName, personPhone);
            Console.WriteLine("The book was loaned successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }

    public void removeBorrowing()
    {
        try
        {
            Console.WriteLine("Book title: ");
            string bookTitle = Console.ReadLine();
            Validator.checkEmptyString(bookTitle);
            Console.WriteLine("Book author: ");
            string bookAuthor = Console.ReadLine();
            Validator.checkEmptyString(bookAuthor);
            Console.WriteLine("Person's phone number: ");
            string personPhone = Console.ReadLine();
            Validator.checkPhoneNumber(personPhone);
            borrowService.deleteBorrow(bookTitle, bookAuthor, personPhone);
            Console.WriteLine("Reader's review!");
            Console.WriteLine("Book rating: ");
            float bookReview = float.Parse(Console.ReadLine());
            Validator.checkFloat(bookReview.ToString());
            reviewService.saveReview(bookTitle, bookAuthor, bookReview);
            Console.WriteLine("The loan was removed successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public void getReviewForBook()
    {
        try
        {
            Console.WriteLine("Book title: ");
            string bookTitle = Console.ReadLine();
            Validator.checkEmptyString(bookTitle);
            Console.WriteLine("Book author: ");
            string bookAuthor = Console.ReadLine();
            Validator.checkEmptyString(bookAuthor);
            var review = reviewService.getRatingForBook(bookTitle, bookAuthor);
            Console.WriteLine("Book review: " + review.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void getRandomRecommandation()
    {
        try
        {
            Console.WriteLine("Genre: ");
            string bookGenre = Console.ReadLine();
            Validator.checkEmptyString(bookGenre);
            var book = bookService.getRandomBookRecomandationByGenre(bookGenre);
            Console.WriteLine("Recommanded book: " + book.toString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void listBooks(IList<Book> books)
    {
        foreach (Book book in books)
        {
            Console.WriteLine(book.toString());
        }
    }

    public void getAllBooks()
    {
        IList<Book> books = bookService.getAllBooks();
        foreach (Book book in books)
        {
            Console.WriteLine(book.toString());
        }
    }

    public void showMenu()
    {
        Console.WriteLine("Welcome to the book managing app!");
        bool on = true;
        while (on)
        {
            Menu();
            Console.WriteLine("Choose an option: ");
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1: 
                    addNewBook();
                    break;
                case 2:
                    removeBook();
                    break;
                case 3:
                    searchBooksByTitle();
                    break;
                case 4:
                    searchBooksByAuthor();
                    break;
                case 5:
                    searchBooksByTitleAndAuthor();
                    break;
                case 6:
                    searchBooksByGenre();
                    break;
                case 7:
                    getAllBooks();
                    break;
                case 8:
                    addNewBorrowing();
                    break;
                case 9:
                    removeBorrowing();
                    break;
                case 10:
                    getReviewForBook();
                    break;
                case 11:
                    getRandomRecommandation();
                    break;
                case 12:
                    on = false;
                    break;
            }
                
        }
    }

    public void Menu()
    {
        Console.WriteLine("1. Add a new book.");
        Console.WriteLine("2. Remove a book.");
        Console.WriteLine("3. Search book by title.");
        Console.WriteLine("4. Search book by author.");
        Console.WriteLine("5. Search book by title and author.");
        Console.WriteLine("6. Search book by genre.");
        Console.WriteLine("7. View all books.");
        Console.WriteLine("8. Make a new loan.");
        Console.WriteLine("9. Remove a loan");
        Console.WriteLine("10. Get a book's rating.");
        Console.WriteLine("11. Get a random book recomandation based on genre.");
        Console.WriteLine("12. Exit.");
    }
}