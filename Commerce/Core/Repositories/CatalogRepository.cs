using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.DatabaseContext.Context.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ecommerce.Commerce.Core.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly IConnection<IMongoDatabase> _mongoConnection;
    private readonly IConnection<Cassandra.ISession> _cassandraConnection;

    public CatalogRepository(IConnection<IMongoDatabase> mongoConnection,
        IConnection<Cassandra.ISession> cassandraConnection)
    {
        _mongoConnection = mongoConnection;
        _cassandraConnection = cassandraConnection;
    }
    public async Task<List<ProductCatalog>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
                .GetCollection<ProductCatalog>("ProductCatalogs")
                .FindAsync(new BsonDocument()))
            .ToList();
    }

    public async Task<ProductCatalog> GetProductById(Guid id)
    {
        var filter = Builders<ProductCatalog>.Filter.Eq(x => x.Id, id);

        return await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .Find(filter).SingleAsync();
    }

    public async Task<List<ProductCatalog>> GetLogByProductId(Guid productId)
    {
        var query = _cassandraConnection.GetConnection().Execute($@"SELECT * FROM catalog WHERE productId = {productId}");

        var log = query.Select(row => new ProductCatalog(row)).ToList();

        return log;
    }

    public async Task Insert(ProductCatalog product)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .InsertOneAsync(product);
    }

    public async Task InsertProductLog(ProductCatalog product)
    {
        var query = await _cassandraConnection.GetConnection()
                                    .PrepareAsync(@"INSERT INTO catalog (id, moment, authorId, productId, price) VALUES (?,?,?,?,?)");
        
        await _cassandraConnection.GetConnection()
            .ExecuteAsync(query.Bind(Guid.NewGuid(), DateTime.Now, product.AuthorId,
                product.ProductId, product.Price));
    }

    public async Task Update(ProductCatalog product)
    {
        var filter = Builders<ProductCatalog>.Filter.Eq(x => x.Id, product.Id);

        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .ReplaceOneAsync(filter, product);
    }

    public async Task Delete(Guid id)
    {
        var filter = Builders<ProductCatalog>.Filter.Eq(x => x.Id, id);

        await _mongoConnection.GetConnection().GetCollection<ProductCatalog>("ProductCatalogs").DeleteManyAsync(filter);
    }
}