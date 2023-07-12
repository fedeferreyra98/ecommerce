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

    public PaymentRepository(EfContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Payment> GetAll()
    {
        return _dbContext.Payment.ToList();
    }

    public Payment GetById(Guid id)
    {
        return _dbContext.Payment.First(x => x.PaymentId == id);
    }

    public void Insert(Payment newPayment)
    {
        _dbContext.Payment.Add(newPayment);
        _dbContext.SaveChanges();
    }
}