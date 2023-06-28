namespace ecommerce.Abstractions;

public interface IService<T> where T : class
{
    T Get(string id);
    void Save(T entity);

}