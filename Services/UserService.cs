using ecommerce.Abstractions;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Services;

public class UserService : IService<User>
{
    private UserRepository _repository;

    public UserService(UserRepository repository)
    {
        _repository = repository;
    }

    public User Get(string id)
    {
        return _repository.Get(id);
    }

    public void Save(User user)
    {
        _repository.Save(user);
    }
}