using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.DatabaseContext.Context.Interface;
using ecommerce.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ecommerce.Commerce.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnection<IMongoDatabase> _mongoConnection;

        public ProductRepository(IConnection<IMongoDatabase> mongoConnection)
        {
            _mongoConnection = mongoConnection;
        }

        public  List<Product> GetAll()
        {
            return ( _mongoConnection.GetConnection()
                    .GetCollection<Product>("Products")
                    .Find(new BsonDocument()))
                .ToList();
        }

        public  Product GetById(Guid id)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.ProductId, id);

            return  _mongoConnection.GetConnection()
                .GetCollection<Product>("Products")
                .Find(filter).SingleOrDefault();
        }

        public void Insert(Product product)
        {
             _mongoConnection.GetConnection()
                .GetCollection<Product>("Products")
                .InsertOne(product);
        }

        public void Update(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.ProductId, product.ProductId);

             _mongoConnection.GetConnection()
                .GetCollection<Product>("Products")
                .ReplaceOne(filter, product);
        }

        public void Delete(Guid id)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.ProductId, id);

             _mongoConnection.GetConnection()
                .GetCollection<Product>("Products")
                .DeleteOne(filter);
        }
    }
}