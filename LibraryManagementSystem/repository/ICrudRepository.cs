using LibraryManagementSystem.model;

namespace LibraryManagementSystem.repository;

public interface ICrudRepository<T> where T : Entity<int>
{
    void save(T entity);
    void delete(int id);
    T findOne(int id);
    void update(int id, T e);
    IList<T> getAll();
}