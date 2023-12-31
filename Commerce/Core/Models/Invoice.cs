using System.ComponentModel.DataAnnotations.Schema;
namespace ecommerce.Commerce.Core.Models;

[Table("Invoices")]
public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public bool Iva { get; set; }
    public int Price { get; set; }
    public bool Payed { get; set; }
}