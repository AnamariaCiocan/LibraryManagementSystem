using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public interface IBookRepository : ICrudRepository<Book>
{
    public IList<Book> findByAuthor(string author);
    public IList<Book> findByTitle(string title);
    public Book findOneByTitleAndAuthor(string title, string author);
    public IList<Book> findByGenre(string genre);

}