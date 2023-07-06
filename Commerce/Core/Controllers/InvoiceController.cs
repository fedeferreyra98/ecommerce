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

    public async Task<List<Invoice>> GetAll()
    {
        return await _invoiceService.GetAllInvoices();
    }

    public async Task<Invoice> GetInvoiceById(Guid invoiceId)
    {
        return await _invoiceService.GetInvoiceById(invoiceId);
    }

    public async void CreateInvoice(Guid userId, Guid orderId)
    {
        var order = _orderService.GetOrderById(orderId).Result;
        var invoice = new Invoice()
        {
            User = userId,
            OrderId = orderId,
            Date = DateTime.Now,
            Iva = order.IVA,
            Payed = false,
            Price = order.FinalPrice
        };
        await _invoiceService.InsertInvoice(invoice);
    }
    
}