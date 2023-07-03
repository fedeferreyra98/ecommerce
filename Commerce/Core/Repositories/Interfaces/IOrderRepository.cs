using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAll();

    Task<Order> GetById(Guid id);

    Task Insert(Order order);

    Task Update(Order order);

    Task Delete(Guid id);
    
    Task<List<Order>> GetAllByStatus(bool status);
}