using ecommerce.Abstractions;
using ecommerce.Models;
using ecommerce.Services;

namespace ecommerce.Controllers;

public class PaymentController : IController<Payment>
{
    private PaymentService _service;

    public PaymentController(PaymentService service)
    {
        _service = service;
    }

    public Payment Get(string id)
    {
        return _service.Get(id);
        //TODO: Renderiza el pago
    }

    public void Save(Payment payment)
    {
        _service.Save(payment);
        
    }
    
    public void Print(Payment entity)
    {
        throw new NotImplementedException();
        //TODO: Mostrar mensaje de pago realizado
    }


}