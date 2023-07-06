namespace ecommerce.Commerce.Core.Models;

public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid User { get; set; }
    public DateTime Date { get; set; }
    public bool Iva { get; set; }
    public decimal Price { get; set; }
    public bool Payed { get; set; }
}