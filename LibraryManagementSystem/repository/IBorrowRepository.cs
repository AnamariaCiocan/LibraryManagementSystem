using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public interface IBorrowRepository : ICrudRepository<Borrow>
{
    public Borrow findOneByBookAndPerson(int idBook, int idPerson);
}