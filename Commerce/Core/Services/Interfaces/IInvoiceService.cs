using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IInvoiceService
{
    Task<List<Invoice>> GetAllInvoices();
    Task<Invoice> GetInvoiceById(Guid invoiceId);

    Task InsertInvoice(Invoice invoice);

    Task UpdateInvoice(Invoice invoiceUpdated);
}