using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface IPaymentRepository
{
    List<Payment> GetAll();

    Payment GetById(Guid id);

    void Insert(Payment newPayment);
}