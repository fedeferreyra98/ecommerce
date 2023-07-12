using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IUserService
{
    List<User> GetAllUsers();

    User GetUserById(Guid id);

    void InsertUser(UserDTO userDTO);

    void UpdateUser(User user);

    void DeleteUser(Guid id);
}