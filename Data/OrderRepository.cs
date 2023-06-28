using ecommerce.Abstractions;
using ecommerce.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ecommerce.Data;

public class OrderRepository : IRepository<Order>
{
    private readonly IMongoCollection<Order> _collection;
    public OrderRepository(IMongoDatabase database, string orderCollection)
    {
        _collection = database.GetCollection<Order>(orderCollection);
    }
    public Order Get(string id)
    {
        return _collection.Find(new BsonDocument("id", id)).FirstOrDefault();
    }

    public void Save(Order entity)
    {
        _collection.InsertOne(entity);
    }

    public void SaveMany(IEnumerable<Order> orders)
    {
        _collection.InsertMany(orders);
    }
}