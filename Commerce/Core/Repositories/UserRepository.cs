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

    public async Task<List<User>> GetAll()
    {
        return await _dbContext.User.ToListAsync();
    }

    public async Task<User> GetById(Guid id)
    {
        return await _dbContext.User.Where(x => x.Id == id).FirstAsync();
    }

    public async Task Insert(User user)
    {
        await _dbContext.User.AddAsync(user);
    }

    public async Task Update(User userUpdated)
    {
        var user = await _dbContext.User.FindAsync(userUpdated.Id);
        
        if (user != null)
        {
            user.Address = userUpdated.Address;
            user.LastName = userUpdated.LastName;
            user.Name = userUpdated.Name;
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var user = await _dbContext.User.FindAsync(id);

        if (user != null) _dbContext.User.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}