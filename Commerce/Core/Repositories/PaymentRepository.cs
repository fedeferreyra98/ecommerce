using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.DatabaseContext.Context;
using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace ecommerce.Commerce.Core.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly EfContext _dbContext;

    public PaymentRepository(IConnection<IMongoDatabase> mongoConnection, EfContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Payment>> GetAll()
    {
        return await _dbContext.Payment.ToListAsync();
    }

    public async Task<Payment> GetById(Guid id)
    {
        return await _dbContext.Payment.Where(x => x.PaymentId == id).FirstAsync();
    }

    public async Task Insert(Payment newPayment)
    {
        await _dbContext.Payment.AddAsync(newPayment);
    }
}