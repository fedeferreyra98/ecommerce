using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ecommerce.DatabaseContext.Context;

public class MongoDataContext : IConnection<IMongoDatabase>
{
    private MongoClient _client;

    private string _dataBaseName;

    public MongoDataContext()
    {
        _client = new MongoClient("mongodb://127.0.0.1:45268");
        _dataBaseName = "ecommerceDb";
    }

    public IMongoDatabase GetConnection()
    {
        return _client.GetDatabase(_dataBaseName);
    }
}