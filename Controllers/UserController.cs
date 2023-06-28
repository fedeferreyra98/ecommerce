using ecommerce.Models;
using ecommerce.Services;

namespace ecommerce.Controllers;

public class UserController
{
    private UserService _service;

    public UserController()
    {
        _service = new UserService();
    }

    public void GetById(string id)
    {
        var user = _service.GetById(id);
        //TODO: Renderiza el usuario o lo que sea...
    }

    public void Save(User user)
    {
        _service.Save(user);
    }
    
    //TODO: Otros metodos importantes
}