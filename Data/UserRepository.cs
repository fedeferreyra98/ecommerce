using System.Text.Json;
using ecommerce.Abstractions;
using ecommerce.Models;
using StackExchange.Redis;

namespace ecommerce.Data;
public class UserRepository: IRepository<User>
{
    private IDatabase _database;

    public UserRepository()
    {
        var redis = ConnectionMultiplexer.Connect("localhost");
        _database = redis.GetDatabase();
    }

    public User Get(string id)
    {
        var user = _database.StringGet(id);
        return JsonSerializer.Deserialize<User>(user);
    }

    public void Save(User user)
    {
        var userJson = JsonSerializer.Serialize(user);
        _database.StringSet(user.Id.ToString(), userJson);
    }
}