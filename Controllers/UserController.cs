using ecommerce.Abstractions;
using ecommerce.Models;
using ecommerce.Services;

namespace ecommerce.Controllers;

public class UserController : IController<User>
{
    private UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    public User Get(string id)
    {
        return _service.Get(id);
    }

    public void Save(User user)
    {
        _service.Save(user);
    }

    public void Print(User entity)
    {
        //TODO: Renderiza el usuario o lo que sea...
        throw new NotImplementedException();
    }
}