using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IPaymentService
{
    Task<List<Payment>> GetAllPayments();

    Task<Payment> GetPaymentById(Guid id);

    Task InsertPayment(Guid orderId, Guid userId, string paymentMethod);
}