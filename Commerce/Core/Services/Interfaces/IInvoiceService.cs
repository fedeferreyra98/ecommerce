using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IInvoiceService
{
    List<Invoice> GetAllInvoices();
    Invoice GetInvoiceById(Guid invoiceId);

    void InsertInvoice(Invoice invoice);

    void UpdateInvoice(Invoice invoiceUpdated);
}