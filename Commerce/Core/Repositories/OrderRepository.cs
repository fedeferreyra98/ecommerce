using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.DatabaseContext.Context.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ecommerce.Commerce.Core.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IConnection<IMongoDatabase> _mongoConnection;

    public OrderRepository(IConnection<IMongoDatabase> mongoConnection)
    {
        _mongoConnection = mongoConnection;
    }
    public List<Order> GetAll()
    {
        return (_mongoConnection.GetConnection()
                .GetCollection<Order>("Orders")
                .Find(new BsonDocument()))
            .ToList();
    }

    public Order GetById(Guid id)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.OrderId, id);

        return _mongoConnection.GetConnection()
            .GetCollection<Order>("Orders")
            .Find(filter).Single();
    }

    public void Insert(Order order)
    {
        _mongoConnection.GetConnection()
            .GetCollection<Order>("Orders")
            .InsertOne(order);
    }

    public void Update(Order order)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);

        _mongoConnection.GetConnection()
            .GetCollection<Order>("Orders")
            .ReplaceOne(filter, order);
    }

    public void Delete(Guid id)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.OrderId, id);

        _mongoConnection.GetConnection()
            .GetCollection<Order>("Orders")
            .DeleteMany(filter);
    }

    public List<Order> GetAllByStatus(bool status)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.OrderStatus, status);

        return _mongoConnection.GetConnection()
            .GetCollection<Order>("Orders")
            .Find(filter).ToList();
    }
}