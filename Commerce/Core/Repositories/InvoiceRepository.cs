using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.DatabaseContext.Context;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Commerce.Core.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly EfContext _dbContext;

    public InvoiceRepository(EfContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Invoice>> GetAll()
    {
        return await _dbContext.Invoice.ToListAsync();
    }

    public async Task<Invoice> GetById(Guid invoiceId)
    {
        return await _dbContext.Invoice.Where(x => x.Id == invoiceId).FirstAsync();
    }

    public async Task Insert(Invoice invoice)
    {
        await _dbContext.Invoice.AddAsync(invoice);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Invoice invoiceUpdated)
    {
        var invoice = await _dbContext.Invoice.FindAsync(invoiceUpdated.Id);
        
        if (invoice != null)
        {
            invoice.User = invoiceUpdated.User;
            invoice.Date = invoiceUpdated.Date;
            invoice.Iva = invoiceUpdated.Iva;
            invoice.Price = invoiceUpdated.Price;
            invoice.OrderId = invoiceUpdated.OrderId;
            invoice.Payed = invoiceUpdated.Payed;
        }
        await _dbContext.SaveChangesAsync();
    }
}