using ecommerce.Abstractions;
using ecommerce.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ecommerce.Data;

public class ProductRepository : IRepository<Product>
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IMongoDatabase database, string productCollection)
    {
        _collection = database.GetCollection<Product>(productCollection);
    }
    
    public Product Get(string id)
    {
        return _collection.Find(new BsonDocument("_id", id)).FirstOrDefault();
    }

    public void Save(Product product)
    {
        _collection.InsertOne(product);
    }

    public void SaveMany(IEnumerable<Product> products)
    {
        _collection.InsertMany(products);
    }
}