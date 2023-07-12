using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface IInvoiceRepository
{
    List<Invoice> GetAll();
    Invoice GetById(Guid invoiceId);

    void Insert(Invoice invoice);

    void Update(Invoice invoiceUpdated);
}