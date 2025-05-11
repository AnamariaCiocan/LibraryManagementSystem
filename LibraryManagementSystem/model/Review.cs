namespace LibraryManagementSystem.model;

public class Review : Entity<int>
{
    private int id;
    private int idBook;
    private float rating;
    public Review(){}

    public Review(int idBook, float rating)
    {
        this.idBook = idBook;
        this.rating = rating;
    }

    public int getIdBook()
    {
        return idBook;
    }

    public float getRating()
    {
        return rating;
    }

    public void setIdBook(int idBook)
    {
        this.idBook = idBook;
    }

    public void setRating(float rating)
    {
        this.rating = rating;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public string toString()
    {
        return "Rating: "+rating;
    }
}