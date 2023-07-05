using Cassandra;
using ecommerce.Cart.Core.Repositories;
using ecommerce.Cart.Core.Repositories.Interfaces;
using ecommerce.Cart.Core.Services;
using ecommerce.Cart.Core.Services.Interfaces;
using ecommerce.Commerce.Core.Repositories;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services;
using ecommerce.Commerce.Core.Services.Interfaces;
using ecommerce.DatabaseContext.Context;
using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StackExchange.Redis;

namespace ecommerce;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(c => c.AddConsole(opt => opt.LogToStandardErrorThreshold = LogLevel.Debug))
            .AddDbContext<EfContext>(ServiceLifetime.Singleton) //Setup databases
            .AddTransient<IConnection<ISession>, CassandraDataContext>()
            .AddTransient<IConnection<ISession>, CartCassandraDataContext>()
            .AddTransient<IConnection<IMongoDatabase>,MongoDataContext>()
            .AddTransient<IConnection<IDatabase>, RedisDataContext>()
            .AddTransient<ICatalogRepository, CatalogRepository>() //setup repositories
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IPaymentRepository, PaymentRepository>()
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IUserCartRepository, UserCartRepository>()
            .AddSingleton<ICatalogService, CatalogService>() //setup services
            .AddSingleton<IOrderService, OrderService>()
            .AddSingleton<IPaymentService, PaymentService>()
            .AddSingleton<IProductService, ProductService>()
            .AddSingleton<IUserService, UserService>()
            .AddSingleton<IUserCartService, UserCartService>()
            .BuildServiceProvider();

        //Configure console logging
        var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        
        logger.LogDebug("Inicia la aplicacion.");
        
    }
}
