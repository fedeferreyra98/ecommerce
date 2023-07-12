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
    public List<ProductCatalog> GetAll()
    {
        return _mongoConnection.GetConnection()
                .GetCollection<ProductCatalog>("ProductCatalogs")
                .Find(new BsonDocument())
            .ToList();
    }

    public ProductCatalog GetProductById(Guid id)
    {
        var filter = Builders<ProductCatalog>.Filter.Eq(x => x.Id, id);

        return _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .Find(filter).Single();
    }

    public List<ProductCatalog> GetLogByProductId(Guid productId)
    {
        var query = _cassandraConnection.GetConnection().Execute($@"SELECT * FROM catalog WHERE productId = {productId}");

        var log = query.Select(row => new ProductCatalog(row)).ToList();

        return log;
    }

    public void Insert(ProductCatalog product)
    {
        _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .InsertOneAsync(product);
    }

    public void InsertProductLog(ProductCatalog product)
    {
        var query = _cassandraConnection.GetConnection()
                                    .Prepare(@"INSERT INTO catalog (id, moment, authorId, productId, price) VALUES (?,?,?,?,?)");
        
        _cassandraConnection.GetConnection()
            .Execute(query.Bind(Guid.NewGuid(), DateTime.Now, product.AuthorId,
                product.ProductId, product.Price));
    }

    public void Update(ProductCatalog product)
    {
        var filter = Builders<ProductCatalog>.Filter.Eq(x => x.Id, product.Id);

        _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .ReplaceOneAsync(filter, product);
    }

    public void Delete(Guid id)
    {
        var filter = Builders<ProductCatalog>.Filter.Eq(x => x.Id, id);

        _mongoConnection.GetConnection().GetCollection<ProductCatalog>("ProductCatalogs").DeleteManyAsync(filter);
    }
}