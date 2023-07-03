using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsers();

    Task<User> GetUserById(Guid id);

    Task InsertUser(UserDTO userDTO);

    Task UpdateUser(User user);

    Task DeleteUser(Guid id);
}