using System.ComponentModel.DataAnnotations.Schema;
namespace ecommerce.Commerce.Core.Models;

[Table("Payments")]
public class Payment
{
    public Guid PaymentId { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public User User { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public int FinalPrice { get; set; }
}