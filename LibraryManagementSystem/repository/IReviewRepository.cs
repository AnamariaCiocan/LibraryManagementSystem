using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public interface IReviewRepository : ICrudRepository<Review>
{
    public IList<Review> getAllReviewsForBook(int idBook);
}