namespace BF2TV.Domain.Repositories;

public interface IJsonRepository<T>
{
    Task<List<T>> GetAll();
    Task Add(T instance);
    Task Remove(T instance);
}