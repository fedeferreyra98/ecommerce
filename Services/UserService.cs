using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Services;

public class UserService
{
    private UserContext _context;

    public UserService()
    {
        _context = new UserContext();
    }

    public User GetById(string id)
    {
        return _context.GetById(id);
    }

    public void Save(User user)
    {
        _context.Save(user);
    }
}