namespace ecommerce.Models;

public class Product
{
    public string Id { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public List<string> Comments { get; set; }
    public List<string> Videos { get; set; }
    public double Price { get; set; }
    // Other properties
}