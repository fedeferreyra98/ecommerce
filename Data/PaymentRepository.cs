using System.Data.Entity;
using ecommerce.Abstractions;
using ecommerce.Models;

namespace ecommerce.Data;

public class PaymentRepository : IRepository<Payment>
{
    private readonly DbContext _context;
    private readonly DbSet<Payment> _dbSet;

    public PaymentRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<Payment>();
    }
    public Payment Get(string id)
    {
        return _dbSet.Find(id) ?? throw new FileNotFoundException();
    }

    public void Save(Payment entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }
}