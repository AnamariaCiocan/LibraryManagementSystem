namespace LibraryManagementSystem.model;

public class Person : Entity<int>
{
    private int id;
    private string name;
    private string phone;

    public Person(string name, string phone)
    {
        this.name = name;
        this.phone = phone;
    }
    public Person(){}

    public int getId()
    {
        return id;
    }

    public string getName()
    {
        return name;
    }

    public string getPhone()
    {
        return phone;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public void setPhone(string phone)
    {
        this.phone = phone;
    }

    public string toString()
    {
        return "Name: "+name + ", phone: "+phone;
    }
}