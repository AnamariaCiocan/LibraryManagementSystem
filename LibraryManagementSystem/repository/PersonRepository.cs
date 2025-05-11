using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public class PersonRepository : IPersonRepository
{
    private readonly IDictionary<string, string> props;

    public PersonRepository(IDictionary<string, string> props)
    {
        this.props = props;
    }
    public void save(Person entity)
    {
        var con = DBUtils.GetConnection(props);
        using var command = con.CreateCommand();
        command.CommandText = "insert into Person values(@id, @name, @phone)";
        var paramId = command.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = GetMaxId() + 1;
        command.Parameters.Add(paramId);
        var paramName = command.CreateParameter();
        paramName.ParameterName = "@name";
        paramName.Value = entity.getName();
        command.Parameters.Add(paramName);
        var paramPhone = command.CreateParameter();
        paramPhone.ParameterName = "@phone";
        paramPhone.Value = entity.getPhone();
        command.Parameters.Add(paramPhone);
        var result = command.ExecuteNonQuery();
        if (result == 0)
        {
            throw new Exception("The person couldn't be added");
        }
    }

    public void delete(int id)
    {
        var con = DBUtils.GetConnection(props);    
        using var command = con.CreateCommand();
        command.CommandText = "delete from Person where id = @id";
        var paramId = command.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = id;
        command.Parameters.Add(paramId);
        var result = command.ExecuteNonQuery();
        if (result == 0)
        {
            throw new Exception("The person couldn't be deleted");
        }
    }

    public Person findOne(int id)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Person where id = @id";
            var paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            command.Parameters.Add(paramId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var name = reader.GetString(1);
                    var phone = reader.GetString(2);
                    var p = new Person(name, phone);
                    p.setId(id);
                    return p;
                }
            }
        }
        return null;
    }

    public void update(int id, Person e)    
    {
        using (var con = DBUtils.GetConnection(props))
        {
            using (var command = con.CreateCommand())
            {
                command.CommandText = "update Person set name = @name, phone = @phone where id = @id";
                var paramName = command.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = e.getName();
                command.Parameters.Add(paramName);
                var paramPhone = command.CreateParameter();
                paramPhone.ParameterName = "@phone";
                paramPhone.Value = e.getPhone();
                command.Parameters.Add(paramPhone);
                var paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.Parameters.Add(paramId);
                var result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("The person couldn't be updated");
                }
            }
        }
    }

    public IList<Person> getAll()
    {
        using (var con = DBUtils.GetConnection(props))
        {
            List<Person> result = new List<Person>();
            var command = con.CreateCommand();
            command.CommandText = "select * from Person";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var idPerson = reader.GetInt32(0);
                var name = reader.GetString(1);
                var phone = reader.GetString(2);
                var p = new Person(name, phone);
                p.setId(idPerson);
                result.Add(p);
            }
            return result;
        }
    }
    
    public Person findOneByPhone(string phone)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Person where phone = @phone";
            var paramPhone = command.CreateParameter();
            paramPhone.ParameterName = "@phone";
            paramPhone.Value = phone;
            command.Parameters.Add(paramPhone);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var p = new Person(name, phone);
                    p.setId(id);
                    return p;
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
            comm.CommandText = "select max(id) from Person";

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