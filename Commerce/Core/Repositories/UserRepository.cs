using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.DatabaseContext.Context;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Commerce.Core.Repositories;
public class UserRepository: IUserRepository
{
    private readonly EfContext _dbContext;

    public UserRepository(EfContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<User> GetAll()
    {
        return _dbContext.User.ToList();
    }

    public User GetById(Guid id)
    {
        return _dbContext.User.First(x => x.Id == id);
    }

    public void Insert(User user)
    {
        _dbContext.User.Add(user);
        _dbContext.SaveChanges();
    }

    public void Update(User userUpdated)
    {
        var user = _dbContext.User.Find(userUpdated.Id);
        
        if (user != null)
        {
            user.Address = userUpdated.Address;
            user.LastName = userUpdated.LastName;
            user.Name = userUpdated.Name;
        }
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var user = _dbContext.User.Find(id);

        if (user != null) _dbContext.User.Remove(user);
        _dbContext.SaveChanges();
    }
}