using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public List<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public User GetUserById(Guid id)
    {
        return _userRepository.GetById(id);
    }

    public void InsertUser(UserDTO userDto)
    {
        var user = new User()
        {
            Name = userDto.Name,
            LastName = userDto.LastName,
            Address = userDto.Address
        };
        _userRepository.Insert(user);
    }

    public void UpdateUser(User user)
    {
        _userRepository.Update(user);
    }

    public void DeleteUser(Guid id)
    {
        _userRepository.Delete(id);
    }
}