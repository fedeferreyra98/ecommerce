namespace ecommerce.Commerce.Core.DTOs;

public class ProductCatalogDTO
{
    public Guid AuthorId { get; set; }

    public Guid ProductId { get; set; }
        
    public DateTime Moment { get; set; }

    public int Price { get; set; }
}