using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IOrderService
{
    List<Order> GetAllOrders();

    Order GetOrderById(Guid id);

    void InsertOrder(OrderDTO order);

    void DeleteOrder(Guid id);

    List<Order> GetOrderByStatus(bool status);

    void ChangeStatus(Order order);
}