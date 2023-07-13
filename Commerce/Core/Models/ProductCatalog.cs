using Cassandra;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ecommerce.Commerce.Core.Models;

public class ProductCatalog
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime Moment { get; set; }
    public int Price { get; set; }
    public ProductCatalog() {}

    public ProductCatalog(Row row)
    {
        Id = row.GetValue<Guid>(nameof(Id).ToLower());
        AuthorId = row.GetValue<Guid>(nameof(AuthorId).ToLower());
        ProductId = row.GetValue<Guid>(nameof(ProductId).ToLower());
        Moment = row.GetValue<DateTime>(nameof(Moment).ToLower());
        Price = row.GetValue<int>(nameof(Price).ToLower());
    }
}