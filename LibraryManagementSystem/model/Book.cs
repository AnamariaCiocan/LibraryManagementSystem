namespace LibraryManagementSystem.model;

public class Book : Entity<int>{ 

    private int id;
    private string title;
    private string author;
    private string genre;
    private int maxQuantity;
    private int currentQuantity;
    public Book(string title, string author, string genre, int quantity)
    {
        this.title = title;
        this.author = author;
        this.genre = genre;
        this.maxQuantity = quantity;
        this.currentQuantity = quantity;
    }
    public Book(){}

    public string getTitle()
    {
        return title;
    }

    public string getAuthor()
    {
        return author;
    }

    public void setTitle(string title)
    {
        this.title = title;
    }

    public void setAuthor(string author)
    {
        this.author = author;
    }
    
    public void setId(int id)
    {
       this.id = id;
    }

    public int getId()
    {
        return id;
    }

    public void setMaxQuantity(int maxQuantity)
    {
        this.maxQuantity = maxQuantity;
    }

    public int getMaxQuantity()
    {
        return maxQuantity;
    }

    public void setCurrentQuantity(int currentQuantity)
    {
        this.currentQuantity = currentQuantity;
    }

    public int getCurrentQuantity()
    {
        return currentQuantity;
    }

    public void setGenre(string genre)
    {
        this.genre = genre;
    }

    public string getGenre()
    {
        return genre;
    }

    public string toString()
    {
        return "Title: "+title+", author: "+author+", genre: "+genre+", stock quantity: "+maxQuantity+", available quantity: "+currentQuantity;
    }
}