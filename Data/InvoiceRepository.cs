using System.Data.Entity;
using ecommerce.Abstractions;
using ecommerce.Models;

namespace ecommerce.Data;

public class InvoiceRepository : IRepository<Invoice>
{
    private readonly DbContext _context;
    private readonly DbSet<Invoice> _dbSet;

    public InvoiceRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<Invoice>();
    }
    public Invoice Get(string id)
    {
        return _dbSet.Find(id) ?? throw new FileNotFoundException();
    }

    public void Save(Invoice entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }
}