using System.Text.Json;
using ecommerce.Abstractions;
using ecommerce.Models;
using StackExchange.Redis;

namespace ecommerce.Data;

public class ShoppingCartRepository : IRepository<ShoppingCart>
{
    private IDatabase _database;

    public ShoppingCartRepository()
    {
        var redis = ConnectionMultiplexer.Connect("localhost");
        _database = redis.GetDatabase();
    }

    public ShoppingCart Get(string id)
    {
        var cart = _database.StringGet(id);
        return JsonSerializer.Deserialize<ShoppingCart>(cart);
    }

    public void Save(ShoppingCart cart)
    {
        var cartJson = JsonSerializer.Serialize(cart);
        _database.StringSet(cart.Id, cartJson);
    }
}