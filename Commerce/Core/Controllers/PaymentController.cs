using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class PaymentController
{
    private readonly IPaymentService? _paymentService;

    public PaymentController(IPaymentService? paymentService)
    {
        _paymentService = paymentService;
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
        Console.WriteLine("Pago realizado con exito!");
    }

}