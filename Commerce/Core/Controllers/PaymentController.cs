using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class PaymentController
{
    private readonly IPaymentService? _paymentService;
    private readonly IInvoiceService _invoiceService;

    public PaymentController(IPaymentService? paymentService, IInvoiceService invoiceService)
    {
        _paymentService = paymentService;
        _invoiceService = invoiceService;
    }

    public async Task<Payment> GetPaymentById(Guid id)
    {
        return await _paymentService.GetPaymentById(id);
    }

    public async Task<List<Payment>> GetAllPayments()
    {
        return await _paymentService.GetAllPayments();
    }

    public async void CreatePayment(Guid orderId, Guid userId, string paymentType)
    {
        await _paymentService.InsertPayment(orderId, userId, paymentType);
        var payedInvoice = _invoiceService.GetAllInvoices().Result.First(x => x.OrderId == orderId);
        payedInvoice.Payed = true;
        await _invoiceService.UpdateInvoice(payedInvoice);
        Console.WriteLine("Pago realizado con exito!");
    }

}