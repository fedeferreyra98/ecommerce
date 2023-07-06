using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface IInvoiceRepository
{
    Task<List<Invoice>> GetAll();
    Task<Invoice> GetById(Guid invoiceId);

    Task Insert(Invoice invoice);

    Task Update(Invoice invoiceUpdated);
}