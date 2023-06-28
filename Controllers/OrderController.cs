using ecommerce.Abstractions;
using ecommerce.Data;
using ecommerce.Models;
using ecommerce.Services;

namespace ecommerce.Controllers;

public class OrderController : IController<Order>
{
    private OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }
    public Order Get(string id)
    {
        return _service.Get(id);
    }

    public void Save(Order entity)
    {
        _service.Save(entity);
    }

    public void Print(Order entity)
    {
        throw new NotImplementedException();
    }
}