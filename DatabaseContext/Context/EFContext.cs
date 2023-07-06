using ecommerce.Commerce.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.DatabaseContext.Context;

public class EfContext : DbContext
{
    public EfContext(DbContextOptions<EfContext> options) : base(options)
    {
    }
    public DbSet<User> User { get; set; }
    public DbSet<Payment> Payment { get; set; }
    public DbSet<Invoice> Invoice { get; set; }

}