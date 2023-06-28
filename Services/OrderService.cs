using ecommerce.Abstractions;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Services;

public class OrderService : IService<Order>
{
    private OrderRepository _repository;

    public OrderService(OrderRepository repository)
    {
        _repository = repository;
    }
    public Order Get(string id)
    {
        return _repository.Get(id);
    }

    public void Save(Order order)
    {
        _repository.Save(order);
    }
}