using ecommerce.DatabaseContext.Context.Interface;
using MongoDB.Driver;

namespace ecommerce.DatabaseContext.Context;

public class MongoDataContext : IConnection<IMongoDatabase>
{
    private MongoClient _client;

    private string _dataBaseName;
    private string _connectionString;

    public MongoDataContext()
    {
        _connectionString =
            "mongodb+srv://grupo3:1234@clusterecommerce.bxf9npm.mongodb.net/?retryWrites=true&w=majority";
        var settings = MongoClientSettings.FromConnectionString(_connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        _client = new MongoClient(settings);
        _dataBaseName = "Ecommerce";
    }
    
    public IMongoDatabase GetConnection()
    {
        return _client.GetDatabase(_dataBaseName);
    }
}