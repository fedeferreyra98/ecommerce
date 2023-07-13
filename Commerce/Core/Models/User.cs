using System.ComponentModel.DataAnnotations.Schema;
namespace ecommerce.Commerce.Core.Models;
[Table("Users")]
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}