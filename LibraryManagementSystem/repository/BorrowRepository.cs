using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public class BorrowRepository : IBorrowRepository
{
    private readonly IDictionary<string, string> props;

    public BorrowRepository(IDictionary<string, string> props)
    {
        this.props = props;
    }

    public void save(Borrow entity)
    {
        var con = DBUtils.GetConnection(props);
        using var command = con.CreateCommand();
        command.CommandText = "insert into Borrow values (@id,@borrowDate,@dueDate,@idBook,@idPerson)";
        var paramId = command.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = GetMaxId() + 1;
        command.Parameters.Add(paramId);
        var paramBorrowDate = command.CreateParameter();
        paramBorrowDate.ParameterName = "@borrowDate";
        paramBorrowDate.Value = entity.getBorrowDate();
        command.Parameters.Add(paramBorrowDate);
        var paramDueDate = command.CreateParameter();
        paramDueDate.ParameterName = "@dueDate";
        paramDueDate.Value = entity.getDueDate();
        command.Parameters.Add(paramDueDate);
        var paramIdBook = command.CreateParameter();
        paramIdBook.ParameterName = "@idBook";
        paramIdBook.Value = entity.getBookId();
        command.Parameters.Add(paramIdBook);
        var paramIdPerson = command.CreateParameter();
        paramIdPerson.ParameterName = "@idPerson";
        paramIdPerson.Value = entity.getUserId();
        command.Parameters.Add(paramIdPerson);
        
        var result = command.ExecuteNonQuery();
        if (result == 0)
        {
            throw new Exception("Borrow could not be saved.");
        }
    }

    public void delete(int id)
    {
        var con = DBUtils.GetConnection(props);
        using var command = con.CreateCommand();
        command.CommandText = "delete from Borrow where id = @id";
        var paramId = command.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = id;
        command.Parameters.Add(paramId);
        var result = command.ExecuteNonQuery();
        if (result == 0)
        {
            throw new Exception("Borrow could not be deleted.");
        }
    }

    public Borrow findOne(int id)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Borrow where id = @id";
            var paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            command.Parameters.Add(paramId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var idB = reader.GetInt32(0);
                    var borrowDate = reader.GetDateTime(1);
                    var dueDate = reader.GetDateTime(2);
                    var bookId = reader.GetInt32(3);
                    var idPerson = reader.GetInt32(4);
                    var borrow = new Borrow(borrowDate, dueDate, bookId,idPerson);
                    borrow.setId(idB);
                    return borrow;
                }
            }
        }

        return null;
    }

    public void update(int id, Borrow e)
    {
        using (var con = DBUtils.GetConnection(props))
        {
            var command = con.CreateCommand();
            command.CommandText = "update Borrow set dueDate = @dueDate where id = @id";
            var paramDate = command.CreateParameter();
            paramDate.ParameterName = "@dueDate";
            paramDate.Value = e.getDueDate();
            command.Parameters.Add(paramDate);
            var paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            command.Parameters.Add(paramId);
            var result = command.ExecuteNonQuery();
            if (result == 0)
            {
                throw new Exception("Borrow could not be updated.");
            }
        }
    }

    public IList<Borrow> getAll()
    {
        using var con = DBUtils.GetConnection(props);
        List<Borrow> borrows = new List<Borrow>();
        using var command = con.CreateCommand();
        command.CommandText = "select * from Borrow";
        using var dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            var borrowDate = dataReader.GetDateTime(1);
            var dueDate = dataReader.GetDateTime(2);
            var bookId = dataReader.GetInt32(3);
            var idPerson = dataReader.GetInt32(4);
            var borrow = new Borrow(borrowDate, dueDate, bookId, idPerson);
            borrow.setId(id);
            borrows.Add(borrow);
        }
        return borrows;
    }

    public Borrow findOneByBookAndPerson(int idBook, int idPerson)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Borrow where idBook = @idBook and idPerson = @idPerson";
            var paramIdBook = command.CreateParameter();
            paramIdBook.ParameterName = "@idBook";
            paramIdBook.Value = idBook;
            command.Parameters.Add(paramIdBook);
            var paramIdPerson = command.CreateParameter();
            paramIdPerson.ParameterName = "@idPerson";
            paramIdPerson.Value = idPerson;
            command.Parameters.Add(paramIdPerson);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var idB = reader.GetInt32(0);
                    var borrowDate = reader.GetDateTime(1);
                    var dueDate = reader.GetDateTime(2);
                    var borrow = new Borrow(borrowDate, dueDate, idBook,idPerson);
                    borrow.setId(idB);
                    return borrow;
                }
            }
        }

        return null;
    }

    private int GetMaxId()
    {
        var con = DBUtils.GetConnection(props);

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "select max(id) from Borrow";

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