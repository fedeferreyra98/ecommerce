using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<User> GetById(Guid id)
    {
        return await _userService.GetUserById(id);
    }

    public async Task<List<User>> GetAll()
    {
        return await _userService.GetAllUsers();
    }

    public async Task Create(UserDTO userDto)
    {
        if (userDto is null) throw new ArgumentNullException(nameof(userDto),"Debe ingresar un usuario.");
        await _userService.InsertUser(userDto);
    }

    public async Task Update(User userUpdated)
    {
        if (userUpdated is null) 
            throw new ArgumentNullException(nameof(userUpdated), "Debe ingresar un usuario.");

        var user = await _userService.GetUserById(userUpdated.Id);
        if (user is null)
            throw new InvalidOperationException("El usuario no existe");
        
        await _userService.UpdateUser(userUpdated);
        Console.WriteLine("El usuario ha sido actualizado correctamente!");
    }

    public async Task Print(User user)
    {
        Console.WriteLine();
        Console.WriteLine($"Nombre: {user.Name}");
        Console.WriteLine($"Apellido: {user.LastName}");
        Console.WriteLine($"Direccion: {user.Address}");
        Console.WriteLine();
    }
}