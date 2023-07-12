using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class InvoiceController
{
    private readonly IInvoiceService _invoiceService;
    private readonly IOrderService _orderService;

    public InvoiceController(IInvoiceService invoiceService, IOrderService orderService)
    {
        _invoiceService = invoiceService;
        _orderService = orderService;
    }

    public List<Invoice> GetAll()
    {
        return _invoiceService.GetAllInvoices();
    }

    public Invoice GetInvoiceById(Guid invoiceId)
    {
        return _invoiceService.GetInvoiceById(invoiceId);
    }

    public void CreateInvoice(Guid userId, Guid orderId)
    {
        var order = _orderService.GetOrderById(orderId);
        var invoice = new Invoice()
        {
            User = userId,
            OrderId = orderId,
            Date = DateTime.Now,
            Iva = order.IVA,
            Payed = false,
            Price = order.FinalPrice
        };
        _invoiceService.InsertInvoice(invoice);
    }
    
}