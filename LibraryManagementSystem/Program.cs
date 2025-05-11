
using System.Configuration;
using log4net.Config;
using LibraryManagementSystem.controller;
using LibraryManagementSystem.repository;
using LibraryManagementSystem.service;

public class main
{
    public static void Main(string[] args)
    { 
        XmlConfigurator.Configure(new FileInfo("App.config"));
        Console.WriteLine("Configuration Settings for libraryDB {0}", GetConnectionStringByName("libraryDB"));
        IDictionary<string, string> props = new SortedList<string, string>();
        props.Add("ConnectionString",GetConnectionStringByName("libraryDB"));

        BookRepository bookRepository = new BookRepository(props);
        PersonRepository personRepository = new PersonRepository(props);
        BorrowRepository borrowRepository = new BorrowRepository(props);
        ReviewRepository reviewRepository = new ReviewRepository(props);
        
        BookService bookService = new BookService(bookRepository);
        PersonService personService = new PersonService(personRepository);
        ReviewService reviewService = new ReviewService(reviewRepository,bookRepository);
        BorrowService borrowService = new BorrowService(borrowRepository,bookRepository,personRepository);
       
        Controller ctrl = new Controller(bookService, personService, borrowService, reviewService);
        ctrl.showMenu();
    }
    private static string GetConnectionStringByName(string name)
    {
        string returnValue = null;
        var settings = ConfigurationManager.ConnectionStrings[name];
        if (settings != null)
            returnValue = settings.ConnectionString;
        return returnValue;
    }
}