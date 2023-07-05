using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ecommerce.DatabaseContext.Context;

public class MongoDataContext : IConnection<IMongoDatabase>
{
    private MongoClient _client;

    private string DataBaseName;

    public MongoDataContext(IConfiguration configuration)
    {
        _client = new MongoClient(configuration.GetValue<string>("Databases:Mongo:ConnectionString"));
        DataBaseName = configuration.GetValue<string>("Databases:Mongo:Database");
    }

    public IMongoDatabase GetConnection()
    {
        return _client.GetDatabase(DataBaseName);
    }
}