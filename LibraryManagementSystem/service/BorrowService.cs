using LibraryManagementSystem.model;
using LibraryManagementSystem.repository;

namespace LibraryManagementSystem.service;

public class BorrowService
{
    private IBorrowRepository borrowRepo;
    private IBookRepository bookRepo;
    private IPersonRepository personRepo;

    public BorrowService(IBorrowRepository borrowRepo, IBookRepository bookRepo, IPersonRepository personRepo)
    {
        this.borrowRepo = borrowRepo;
        this.bookRepo = bookRepo;
        this.personRepo = personRepo;
    }

    public void saveBorrow(DateTime borrowDate, DateTime dueDate, string nameBook, string authorBook, string name,
        string phone)
    {
          Book book = bookRepo.findOneByTitleAndAuthor(nameBook, authorBook);
          if (book != null)
          {   lendBook(nameBook,authorBook); 
              Person person = personRepo.findOneByPhone(phone);
              if (person == null)
              {
                   person = new Person(name, phone);
                   personRepo.save(person);
              }
              else
              {
                  if (borrowRepo.findOneByBookAndPerson(book.getId(), person.getId()) != null)
                  {
                      throw new Exception("Borrow already exists");
                  }
              }
              Borrow borrow = new Borrow();
              borrow.setBorrowDate(borrowDate);
              borrow.setDueDate(dueDate);
              borrow.setUserId(personRepo.findOneByPhone(person.getPhone()).getId());
              borrow.setBookId(book.getId());
                   
              borrowRepo.save(borrow);
          }
          else
          {
              throw new Exception("Book could not be found");
          }
    }

    public void deleteBorrow(string nameBook, string authorBook, string phone)
    {
        Book book = bookRepo.findOneByTitleAndAuthor(nameBook, authorBook);
        if (book != null)
        {returnBook(nameBook,authorBook);
            Person person = personRepo.findOneByPhone(phone);
            if (person == null)
            {
                throw new Exception("Person doesn't exists");
            }
            else
            {
                Borrow borrow = borrowRepo.findOneByBookAndPerson(book.getId(), person.getId());
                if ( borrow == null)
                {
                   throw new Exception("The stock is already full!");
                }
                
                borrowRepo.delete(borrow.getId());
            }
        }
        else
        {
            throw new Exception("Book could not be found");
        }
    }
    
    public void returnBook(string title, string author)
    {
        Book book = new Book();
        book = bookRepo.findOneByTitleAndAuthor(title, author);
        if (book.getCurrentQuantity() != book.getMaxQuantity())
        {
            book.setCurrentQuantity(book.getCurrentQuantity()+1);
            bookRepo.update(book.getId(), book);
            Console.WriteLine("Book returned succesfully");
        }
        else
        {
            throw new Exception("The stock is full for this book");
        }
    }

    public void lendBook(string title, string author)
    {
        Book book = new Book(); 
        book = bookRepo.findOneByTitleAndAuthor(title, author);
        if (book.getCurrentQuantity() > 0)
        {
            book.setCurrentQuantity(book.getCurrentQuantity() - 1);
            bookRepo.update(book.getId(), book);
        }
        else
        {
            throw new Exception("Book out of stock");
        }
    }

}