using System.Data;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace LibraryManagementSystem.repository;

public class DBUtils
{
    private static IDbConnection instance = null;
    
    public static IDbConnection GetConnection(IDictionary<string, string> props)
    {
        instance = GetNewConnection(props);
        instance.Open();
        return instance;
    }

    private static IDbConnection GetNewConnection(IDictionary<string, string> props)
    {
        return ConnectionFactory.GetInstance().CreateConnection(props);
    }
}
public abstract class ConnectionFactory
{
    protected ConnectionFactory()
    {
    }

    private static ConnectionFactory _instance;

    public static ConnectionFactory GetInstance()
    {
        if (_instance != null) return _instance;
        var assem = Assembly.GetExecutingAssembly();
        var types = assem.GetTypes();
        foreach (var type in types)
        {
            if (type.IsSubclassOf(typeof(ConnectionFactory)))
                _instance = (ConnectionFactory)Activator.CreateInstance(type);
        }

        return _instance;
    }

    public abstract IDbConnection CreateConnection(IDictionary<string, string> props);
}

public class SqliteConnectionFactory : ConnectionFactory
{
    public override IDbConnection CreateConnection(IDictionary<string, string> props)
    {
        //Console.WriteLine("creating sqlite connection");
        var connectionString = props["ConnectionString"];
        return new SqliteConnection(connectionString);
    }
}