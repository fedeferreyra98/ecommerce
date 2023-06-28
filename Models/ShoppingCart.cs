namespace ecommerce.Models;

public class ShoppingCart
{
    public string Id { get; set; }
    public Dictionary<Product, int> Items { get; set; } 
    // Product and quantity
}