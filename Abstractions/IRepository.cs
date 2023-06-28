namespace ecommerce.Abstractions;

public interface IRepository<T> where T : class
{
    T Get(string id);
    void Save(T entity);
}
