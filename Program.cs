using System.Globalization;
using ecommerce.Commerce.Core.Repositories;
using ecommerce.Commerce.Core.Repositories.Contexts;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection();

        //setup our Databases
        serviceProvider.AddDbContext<EfContext>(ServiceLifetime.Singleton);

    }
}