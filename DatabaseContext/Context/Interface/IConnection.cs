namespace ecommerce.DatabaseContext.Context.Interface;

public interface IConnection<T> where T: class
{
    public T GetConnection();
}