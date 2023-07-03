using ecommerce.Commerce.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Commerce.Core.Repositories.Contexts;

public class EfContext : DbContext
{
    public EfContext(DbContextOptions<EfContext> options) : base(options)
    {
    }
    public DbSet<User> User { get; set; }

}