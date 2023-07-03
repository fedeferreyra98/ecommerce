using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Contexts.Interfaces;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ecommerce.Commerce.Core.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly IConnection<IMongoDatabase> _mongoConnection;

    public PaymentRepository(IConnection<IMongoDatabase> mongoConnection)
    {
        _mongoConnection = mongoConnection;
    }
    public async Task<List<Payment>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
                .GetCollection<Payment>("Payments")
                .FindAsync(new BsonDocument()))
            .ToList();
    }

    public async Task<Payment> GetById(Guid id)
    {
        var filter = Builders<Payment>.Filter.Eq(x => x.PaymentId, id);

        return await _mongoConnection.GetConnection()
            .GetCollection<Payment>("Payments")
            .Find(filter).SingleAsync();
    }

    public async Task Insert(Payment newPayment)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<Payment>("Payments")
            .InsertOneAsync(newPayment);
    }
}