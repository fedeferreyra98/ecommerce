using ecommerce.Utils;

namespace ecommerce.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Dni { get; set; }
    public TimeSpan Activity { get; set; }
    public UserTypes Type { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    
    public User()
    {
        Type = UserTypes.Low;
        Activity = TimeSpan.Zero;
    }
}