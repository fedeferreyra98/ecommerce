namespace ecommerce.Commerce.Core.DTOs;

public class OrderDTO
{
    public Guid UserId { get; set; }

    public List<ProductCartDTO> Products { get; set; }

    public bool IVA { get; set; }
}