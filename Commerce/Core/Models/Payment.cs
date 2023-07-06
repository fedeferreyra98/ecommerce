namespace ecommerce.Commerce.Core.Models;

public class Payment
{
    public Guid PaymentId { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public User User { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
}