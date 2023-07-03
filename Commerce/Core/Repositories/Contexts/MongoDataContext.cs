using MongoDB.Driver;

namespace ecommerce.Commerce.Core.Repositories.Contexts;

public class MongoDataContext
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