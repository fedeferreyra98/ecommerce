namespace ecommerce.DatabaseContext.Context.Interfaces
{
    public interface IConnection<T> where T : class
    {
        T GetConnection();
    }
}

