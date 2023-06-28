using ecommerce.Abstractions;
using ecommerce.Models;
using ecommerce.Services;

namespace ecommerce.Controllers;

public class InvoiceController : IController<Invoice>
{
    private InvoiceService _service;

    public InvoiceController(InvoiceService service)
    {
        _service = service;
    }
    public Invoice Get(string id)
    {
        return _service.Get(id);
    }

    public void Save(Invoice entity)
    {
        _service.Save(entity);
    }

    public void Print(Invoice entity)
    {
        throw new NotImplementedException();
    }
}