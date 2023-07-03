namespace ecommerce.Models;

public class ShoppingCart
{
    public string Id { get; set; }
    public Dictionary<string, int> Products { get; set; } // ProductId and quantity

    public ShoppingCart(string id)
    {
        Id = id;
        Products = new Dictionary<string, int>();
    }
}