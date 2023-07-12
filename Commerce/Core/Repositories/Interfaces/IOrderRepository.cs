using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface IOrderRepository
{
    List<Order> GetAll();

    Order GetById(Guid id);

    void Insert(Order order);

    void Update(Order order);

    void Delete(Guid id);
    
    List<Order> GetAllByStatus(bool status);
}