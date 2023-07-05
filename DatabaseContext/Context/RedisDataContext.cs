using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace ecommerce.DatabaseContext.Context;

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