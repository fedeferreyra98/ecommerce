namespace ecommerce.Cart.Core.Repositories.Context.Interfaces
{
    public interface IConnection<T> where T : class
    {
        T GetConnection();
    }
}

