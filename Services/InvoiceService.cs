using ecommerce.Abstractions;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Services;

public class InvoiceService : IService<Invoice>
{
    private InvoiceRepository _repository;

    public InvoiceService(InvoiceRepository repository)
    {
        _repository = repository;
    }
    public Invoice Get(string id)
    {
        return _repository.Get(id);
    }

    public void Save(Invoice invoice)
    {
        _repository.Save(invoice);
    }
}