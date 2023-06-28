namespace ecommerce.Models;

public class Order
{
    public string Id { get; set; }
    public User User { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public double TotalAmount { get; set; }
    public double Discount { get; set; }
    public double Taxes { get; set; }
    // Other properties like order status, delivery details, etc.
}