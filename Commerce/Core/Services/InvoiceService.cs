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

    public async Task<List<Invoice>> GetAllInvoices()
    {
        return await _invoiceRepository.GetAll();
    }

    public async Task<Invoice> GetInvoiceById(Guid invoiceId)
    {
        return await _invoiceRepository.GetById(invoiceId);
    }

    public async Task InsertInvoice(Invoice invoice)
    {
        await _invoiceRepository.Insert(invoice);
    }

    public async Task UpdateInvoice(Invoice invoiceUpdated)
    {
        await _invoiceRepository.Update(invoiceUpdated);
    }
}