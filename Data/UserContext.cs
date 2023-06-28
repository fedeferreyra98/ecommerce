using System.Text.Json;
using ecommerce.Models;
using StackExchange.Redis;

namespace ecommerce.Data;
public class UserContext
{
    private IDatabase _database;

    public UserContext()
    {
        var redis = ConnectionMultiplexer.Connect("localhost");
        _database = redis.GetDatabase();
    }

    public User GetById(string id)
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