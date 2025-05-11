using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public interface IPersonRepository : ICrudRepository<Person>
{
    Person findOneByPhone(string phone);
}