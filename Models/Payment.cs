namespace ecommerce.Models;

public class Payment
{
    public string Id { get; set; }
    public Invoice Invoice { get; set; }
    public DateTime PaymentDate { get; set; }
    public double Amount { get; set; }
    public string PaymentMethod { get; set; }
    // Other properties
}