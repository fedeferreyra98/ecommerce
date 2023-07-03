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
    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User> GetUserById(Guid id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task InsertUser(UserDTO userDto)
    {
        var user = new User()
        {
            Name = userDto.Name,
            LastName = userDto.LastName,
            Address = userDto.Address
        };
        await _userRepository.Insert(user);
    }

    public async Task UpdateUser(User user)
    {
        await _userRepository.Update(user);
    }

    public async Task DeleteUser(Guid id)
    {
        await _userRepository.Delete(id);
    }
}