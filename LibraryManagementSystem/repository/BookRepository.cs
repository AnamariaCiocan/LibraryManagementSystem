using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public class BookRepository: IBookRepository
{
    private readonly IDictionary<string, string> props;

    public BookRepository(IDictionary<string, string> props)
    {
        this.props = props;
    }
    
    public void save(Book entity)
    {
        var con = DBUtils.GetConnection(props);
        using var command = con.CreateCommand();
        command.CommandText = "insert into Book values (@id,@title,@author,@genre,@quantity,@currentQuantity)";

        var paramId = command.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = GetMaxId() + 1;
        command.Parameters.Add(paramId);
        
        var paramTitle = command.CreateParameter();
        paramTitle.ParameterName = "@title";
        paramTitle.Value = entity.getTitle();
        command.Parameters.Add(paramTitle);
        
        var paramAuthor = command.CreateParameter();
        paramAuthor.ParameterName = "@author";
        paramAuthor.Value = entity.getAuthor();
        command.Parameters.Add(paramAuthor);
        
        var paramGenre = command.CreateParameter();
        paramGenre.ParameterName = "@genre";
        paramGenre.Value = entity.getGenre();
        command.Parameters.Add(paramGenre);
        
        var paramQuantity = command.CreateParameter();
        paramQuantity.ParameterName = "@quantity";
        paramQuantity.Value = entity.getMaxQuantity();
        command.Parameters.Add(paramQuantity);
        
        var paramCurrentQuantity = command.CreateParameter();
        paramCurrentQuantity.ParameterName = "@currentQuantity";
        paramCurrentQuantity.Value = entity.getMaxQuantity();
        command.Parameters.Add(paramCurrentQuantity);
 
        var result = command.ExecuteNonQuery();
        if (result == 0)
        {
            throw new Exception("Book could not be saved");
        }
    }

    public void delete(int id)
    { 
        var con = DBUtils.GetConnection(props);
        using var command = con.CreateCommand();
        command.CommandText = "delete from Book where id = @id";
        var paramId = command.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = id;
        command.Parameters.Add(paramId);
        var result = command.ExecuteNonQuery();
        if (result == 0)
        {
            throw new Exception("Book could not be deleted");
        }
    }

    public Book findOne(int id)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Book where id = @id";
            var paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            command.Parameters.Add(paramId);

            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    var title = dataReader.GetString(1);
                    var author = dataReader.GetString(2);
                    var genre = dataReader.GetString(3);
                    var quantity = dataReader.GetInt32(4);
                    var currentQuantity = dataReader.GetInt32(5);
                    var book = new Book(title, author, genre, quantity);
                    book.setId(id);
                    book.setCurrentQuantity(currentQuantity);
                    return book;
                }
            }
        }
        return null;
    }

    public IList<Book> findByAuthor(string author)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Book where author = @author";
            var paramAuthor = command.CreateParameter();
            paramAuthor.ParameterName = "@author";
            paramAuthor.Value = author;
            command.Parameters.Add(paramAuthor);

            using (var dataReader = command.ExecuteReader())
            { List<Book> books = new List<Book>();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var title = dataReader.GetString(1);
                        var genre = dataReader.GetString(3);
                        var quantity = dataReader.GetInt32(4);
                        var currentQuantity = dataReader.GetInt32(5);
                        var book = new Book(title, author, genre,quantity);
                        book.setId(id);
                        book.setCurrentQuantity(currentQuantity);
                        books.Add(book);
                    }

                    if (books.Count != 0)
                    {
                        return books;
                    }
            }
        }
        return null;
    }
    
    public IList<Book> findByTitle(string title)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Book where title = @title";
            var paramTitle = command.CreateParameter();
            paramTitle.ParameterName = "@title";
            paramTitle.Value = title;
            command.Parameters.Add(paramTitle);

            using (var dataReader = command.ExecuteReader())
            {
                List<Book> books = new List<Book>();
                while (dataReader.Read())
                {
                    var id = dataReader.GetInt32(0);
                    var author = dataReader.GetString(2);
                    var genre = dataReader.GetString(3);
                    var quantity = dataReader.GetInt32(4);
                    var currentQuantity = dataReader.GetInt32(5);
                    var book = new Book(title, author, genre,quantity);
                    book.setId(id);
                    book.setCurrentQuantity(currentQuantity);
                    books.Add(book);
                }

                if (books.Count != 0)
                {
                    return books;
                }
            }
        }
        return null;
    }
    
    public IList<Book> findByGenre(string genre)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Book where genre = @genre";
            var paramGenre = command.CreateParameter();
            paramGenre.ParameterName = "@genre";
            paramGenre.Value = genre;
            command.Parameters.Add(paramGenre);

            using (var dataReader = command.ExecuteReader())
            {
                List<Book> books = new List<Book>();
                while (dataReader.Read())
                {
                    var id = dataReader.GetInt32(0);
                    var title = dataReader.GetString(1);
                    var author = dataReader.GetString(2);
                    var quantity = dataReader.GetInt32(4);
                    var currentQuantity = dataReader.GetInt32(5);
                    var book = new Book(title, author, genre,quantity);
                    book.setId(id);
                    book.setCurrentQuantity(currentQuantity);
                    books.Add(book);
                }

                if (books.Count != 0)
                {
                    return books;
                }
            }
        }
        return null;
    }
    
    public Book findOneByTitleAndAuthor(string title, string author)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Book where title = @title and author = @author";
            var paramTitle = command.CreateParameter();
            paramTitle.ParameterName = "@title";
            paramTitle.Value = title;
            command.Parameters.Add(paramTitle);
            var paramAuthor = command.CreateParameter();
            paramAuthor.ParameterName = "@author";
            paramAuthor.Value = author;
            command.Parameters.Add(paramAuthor);

            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                     var id = dataReader.GetInt32(0);
                     var genre = dataReader.GetString(3);
                     var quantity = dataReader.GetInt32(4);
                     var currentQuantity = dataReader.GetInt32(5);
                     var book = new Book(title, author, genre, quantity);
                     book.setId(id);
                     book.setCurrentQuantity(currentQuantity);
                     return book;
                }
                   
            }
            
        }
        return null;
    }
    
    public void update(int id, Book book)
    {
        using(var con = DBUtils.GetConnection(props))
        {
            using (var command = con.CreateCommand())
            {
                command.CommandText = "update Book set currentQuantity = @currentQuantity where id = @id";
                var paramCurrentQuantity = command.CreateParameter();
                paramCurrentQuantity.ParameterName = "@currentQuantity";
                paramCurrentQuantity.Value = book.getCurrentQuantity();
                command.Parameters.Add(paramCurrentQuantity);   
                var paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.Parameters.Add(paramId);
                var result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Book could not be updated because of invalid id");
                }
            }
        }
    }

    public IList<Book> getAll()
    {
        using var con = DBUtils.GetConnection(props);
        List<Book> books = new List<Book>();
        using var command = con.CreateCommand();
        command.CommandText = "select * from Book";
        using var dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            var title = dataReader.GetString(1);
            var author = dataReader.GetString(2);
            var genre = dataReader.GetString(3);
            var quantity = dataReader.GetInt32(4);
            var currentQuantity = dataReader.GetInt32(5);
            var book = new Book(title, author, genre,quantity);
            book.setId(id);
            book.setCurrentQuantity(currentQuantity);
            books.Add(book);
        }

        return books;
    }
    private int GetMaxId()
    {
        var con = DBUtils.GetConnection(props);

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT MAX(id) FROM Book";

            using (var dataR = comm.ExecuteReader())
            {
                if (dataR.Read())
                {
                    if (dataR.IsDBNull(0))
                    {
                        return 0;
                    }
                
                    var maxId = dataR.GetInt32(0);
                    return maxId;
                }
            }
        }
        return 0;
    }
}