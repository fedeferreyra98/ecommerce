using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.DatabaseContext.Context;

namespace ecommerce.Commerce.Core.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly EfContext _dbContext;

    public InvoiceRepository(EfContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public List<Invoice> GetAll()
    {
        return _dbContext.Invoice.ToList();
    }

    public Invoice GetById(Guid invoiceId)
    {
        return _dbContext.Invoice.First(x => x.Id == invoiceId);
    }

    public void Insert(Invoice invoice)
    {
        _dbContext.Invoice.Add(invoice);
        _dbContext.SaveChanges();
    }

    public void Update(Invoice invoiceUpdated)
    {
        var invoice = _dbContext.Invoice.Find(invoiceUpdated.Id);
        
        if (invoice != null)
        {
            invoice.User = invoiceUpdated.User;
            invoice.Date = invoiceUpdated.Date;
            invoice.Iva = invoiceUpdated.Iva;
            invoice.Price = invoiceUpdated.Price;
            invoice.OrderId = invoiceUpdated.OrderId;
            invoice.Payed = invoiceUpdated.Payed;
        }
        _dbContext.SaveChanges();
    }
}