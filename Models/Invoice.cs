namespace ecommerce.Models;

public class Invoice
{
    public string Id { get; set; }
    public Order Order { get; set; }
    public DateTime IssueDate { get; set; }
    public double Amount { get; set; }
    public bool IsPaid { get; set; }
    // Other properties
}