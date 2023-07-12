using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface IUserRepository
{
    List<User> GetAll();

    User GetById(Guid id);

    void Insert(User user);
    
    void Update(User user);

    void Delete(Guid id);
}