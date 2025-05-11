using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public class ReviewRepository : IReviewRepository
{
    private readonly IDictionary<string, string> props;

    public ReviewRepository(IDictionary<string, string> props)
    {
        this.props = props;
    }
    public void save(Review entity)
    {
        var con = DBUtils.GetConnection(props);
        using var command = con.CreateCommand();
        command.CommandText = "insert into Review values (@id,@idBook,@rating)";

        var paramId = command.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = GetMaxId()+1;
        command.Parameters.Add(paramId);
        var paramIdBook = command.CreateParameter();
        paramIdBook.ParameterName = "@idBook";
        paramIdBook.Value = entity.getIdBook();
        command.Parameters.Add(paramIdBook);
        var paramRating = command.CreateParameter();
        paramRating.ParameterName = "@rating";
        paramRating.Value = entity.getRating();
        command.Parameters.Add(paramRating);
        var result = command.ExecuteNonQuery();
        if (result == 0)
        {
            throw new Exception("Review could not be saved");
        }
    }

    public void delete(int id)
    {
        var con = DBUtils.GetConnection(props);
        using var command = con.CreateCommand();
        command.CommandText = "delete from Review where id = @id";
        var paramId = command.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = id;
        command.Parameters.Add(paramId);
        var result = command.ExecuteNonQuery();
        if (result == 0)
        {
            throw new Exception("Review could not be deleted");
        }
    }

    public Review findOne(int id)
    {
        var con = DBUtils.GetConnection(props);
        using (var command = con.CreateCommand())
        {
            command.CommandText = "select * from Review where id = @id";
            var paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            command.Parameters.Add(paramId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var idBook = reader.GetInt32(1);
                    var rating = reader.GetFloat(2);
                    var review = new Review(idBook, rating);
                    review.setId(id); 
                    return review;
                }
            }
        }

        return null;
    }

    public void update(int id, Review e)
    {
        //not needed
    }

    public IList<Review> getAll()
    {
        using var con = DBUtils.GetConnection(props);
        List<Review> reviews = new List<Review>();
        using var command = con.CreateCommand();
        command.CommandText = "select * from Review";
        using var dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            var idBook = dataReader.GetInt32(1);
            var rating = dataReader.GetFloat(2);
            var review = new Review(idBook,rating);
            review.setId(id);
            reviews.Add(review);
        }
        return reviews;
    }

    public IList<Review> getAllReviewsForBook(int idBook)
    {
        using var con = DBUtils.GetConnection(props);
        List<Review> reviews = new List<Review>();
        using var command = con.CreateCommand();
        command.CommandText = "select * from Review where idBook = @idBook";
        var paramIdBook = command.CreateParameter();
        paramIdBook.ParameterName = "@idBook";
        paramIdBook.Value = idBook;
        command.Parameters.Add(paramIdBook);
        using var dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            var rating = dataReader.GetFloat(2);
            var review = new Review(idBook,rating);
            review.setId(id);
            reviews.Add(review);
        }
        return reviews;
        
    }
    private int GetMaxId()
    {
        var con = DBUtils.GetConnection(props);

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "select max(id) from Review";

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