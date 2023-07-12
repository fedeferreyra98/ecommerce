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

    public Payment GetPaymentById(Guid id)
    {
        return _paymentService.GetPaymentById(id);
    }

    public List<Payment> GetAllPayments()
    {
        return _paymentService.GetAllPayments();
    }

    public void CreatePayment(Guid orderId, Guid userId, string paymentType)
    {
        _paymentService.InsertPayment(orderId, userId, paymentType);
        var payedInvoice = _invoiceService.GetAllInvoices().First(x => x.OrderId == orderId);
        payedInvoice.Payed = true;
        _invoiceService.UpdateInvoice(payedInvoice);
        Console.WriteLine("Pago realizado con exito!");
    }

}