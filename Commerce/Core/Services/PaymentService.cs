using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;

    public PaymentService(IPaymentRepository paymentRepository, IUserService userService, IOrderService orderService)
    {
        _paymentRepository = paymentRepository;
        _userService = userService;
        _orderService = orderService;
    }
    public async Task<List<Payment>> GetAllPayments()
    {
        return await _paymentRepository.GetAll();
    }

    public async Task<Payment> GetPaymentById(Guid id)
    {
        var payment = await _paymentRepository.GetById(id);

        if (payment is null) throw new ApplicationException("El pago no existe");
       
        return payment;
    }

    public async Task InsertPayment(Guid orderId, Guid userId, string paymentMethod)
    {
        var order = await _orderService.GetOrderById(orderId);
        order.OrderStatus = true;
        await _orderService.ChangeStatus(order);
      
        var user = await _userService.GetUserById(userId);

        Payment newPayment = new()
        {
            OrderId = orderId,
            User = user,
            PaymentDate = DateTime.Now,
            PaymentMethod = paymentMethod,
            FinalPrice = order.FinalPrice
        };

        await _paymentRepository.Insert(newPayment);
    }
}