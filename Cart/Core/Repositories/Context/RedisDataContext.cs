using ecommerce.Cart.Core.Repositories.Context.Interfaces;
using StackExchange.Redis;

namespace ecommerce.Cart.Core.Repositories.Context;

public class RedisDataContext : IConnection<IDatabase>
{
    public ConnectionMultiplexer _connection;

    public RedisDataContext(IConfiguration configuration)
    {
        _connection = ConnectionMultiplexer.Connect(configuration.GetValue<string>("Databases:Redis:ConnectionString"));
    }

    public IDatabase GetConnection()
    {
        return _connection.GetDatabase();
    }
}