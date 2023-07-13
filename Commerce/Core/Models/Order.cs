using MongoDB.Bson.Serialization.Attributes;

namespace ecommerce.Commerce.Core.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid OrderId { get; set; }

    public DateTime TimeStamp { get; set; }

    public User User { get; set; }

    public List<ProductCart> Products { get; set; }

    public bool IVA { get; set; }

    public bool OrderStatus { get; set; } = false;

    public int FinalPrice { get; set; }
}