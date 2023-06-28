using ecommerce.Abstractions;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Services;

public class PaymentService : IService<Payment>
{
    private PaymentRepository _repository;

    public PaymentService(PaymentRepository repository)
    {
        _repository = repository;
    }

    public Payment Get(string id)
    {
        return _repository.Get(id);
    }

    public void Save(Payment payment)
    {
        _repository.Save(payment);
    }
}