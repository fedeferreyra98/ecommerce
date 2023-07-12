using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class UserController
{
    private readonly IUserService? _userService;

    public UserController(IUserService? userService)
    {
        _userService = userService;
    }
    public User GetById(Guid id)
    {
        return _userService.GetUserById(id);
    }

    public List<User> GetAll()
    {
        return _userService.GetAllUsers();
    }

    public void Create(UserDTO userDto)
    {
        if (userDto is null) throw new ArgumentNullException(nameof(userDto),"Debe ingresar un usuario.");
        _userService.InsertUser(userDto);
    }

    public void Update(User userUpdated)
    {
        if (userUpdated is null) 
            throw new ArgumentNullException(nameof(userUpdated), "Debe ingresar un usuario.");

        var user = _userService.GetUserById(userUpdated.Id);
        if (user is null)
            throw new InvalidOperationException("El usuario no existe");
        
        _userService.UpdateUser(userUpdated);
        Console.WriteLine("El usuario ha sido actualizado correctamente!");
    }

    public static void Print(User user)
    {
        Console.WriteLine();
        Console.WriteLine($"Nombre: {user.Name}");
        Console.WriteLine($"Apellido: {user.LastName}");
        Console.WriteLine($"Direccion: {user.Address}");
        Console.WriteLine();
    }
}