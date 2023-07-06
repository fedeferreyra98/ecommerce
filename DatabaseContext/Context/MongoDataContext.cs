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
        _client = new MongoClient("mongodb://127.0.0.1:45268");
        DataBaseName = "ecommerceDb";
    }

    public IMongoDatabase GetConnection()
    {
        return _client.GetDatabase(DataBaseName);
    }
}