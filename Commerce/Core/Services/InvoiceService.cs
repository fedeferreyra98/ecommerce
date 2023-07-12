using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceService(IUserService userService, IOrderService orderService, IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public List<Invoice> GetAllInvoices()
    {
        return _invoiceRepository.GetAll();
    }

    public Invoice GetInvoiceById(Guid invoiceId)
    {
        return _invoiceRepository.GetById(invoiceId);
    }

    public void InsertInvoice(Invoice invoice)
    {
        _invoiceRepository.Insert(invoice);
    }

    public void UpdateInvoice(Invoice invoiceUpdated)
    {
        _invoiceRepository.Update(invoiceUpdated);
    }
}