using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IPaymentService
{
    List<Payment> GetAllPayments();

    Payment GetPaymentById(Guid id);

    void InsertPayment(Guid orderId, Guid userId, string paymentMethod);
}