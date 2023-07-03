using MongoDB.Bson.Serialization.Attributes;
namespace ecommerce.Commerce.Core.Models;

public class Payment
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid PaymentId { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public User User { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
}