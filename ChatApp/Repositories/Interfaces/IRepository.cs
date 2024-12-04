namespace ChatApp.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<T?> Get(Guid id);
    Task<T> Create(T obj);
    Task<T> Update(T obj);
    Task Delete(T obj);
}