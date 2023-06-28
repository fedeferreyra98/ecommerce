namespace ecommerce.Abstractions;

public interface IController<T> where T : class
{
    T Get(string id);

    void Save(T entity);

    void Print(T entity);
}