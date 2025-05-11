namespace LibraryManagementSystem.model;

public class Borrow : Entity<int>
{
    private int id;
    private DateTime borrowDate;
    private DateTime dueDate;
    private int bookId;
    private int personId;
    
    public Borrow(){}

    public Borrow(DateTime borrowDate, DateTime dueDate, int bookId, int userId)
    {
        this.borrowDate = borrowDate;
        this.dueDate = dueDate;
        this.bookId = bookId;
        this.personId = userId;
    }

    public void setBorrowDate(DateTime borrowDate)
    {
        this.borrowDate = borrowDate;
    }

    public DateTime getBorrowDate()
    {
        return borrowDate;
    }

    public void setDueDate(DateTime dueDate)
    {
        this.dueDate = dueDate;
    }

    public DateTime getDueDate()
    {
        return dueDate;
    }

    public void setBookId(int bookId)
    {
        this.bookId = bookId;
    }

    public void setUserId(int userId)
    {
        this.personId = userId;
    }

    public int getBookId()
    {
        return bookId;
    }

    public int getUserId()
    {
        return personId;
    }
    
    public void setId(int id)
    {
        this.id = id;
    }

    public int getId()
    {
        return id;
    }
}