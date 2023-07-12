using Cassandra;
using ecommerce.Cart.Core.Dtos;
using ecommerce.Cart.Core.Repositories.Interfaces;
using ecommerce.DatabaseContext.Context.Interface;
using StackExchange.Redis;

namespace ecommerce.Cart.Core.Repositories;

public class UserCartRepository : IUserCartRepository
{
    private readonly IConnection<IDatabase> _redisConnection;
        private readonly IConnection<ISession> _cassandraConnection;

        public UserCartRepository(IConnection<IDatabase> redisConnection, IConnection<ISession> cassandraConnection)
        {
            _redisConnection = redisConnection;
            _cassandraConnection = cassandraConnection;
        }

        public Guid ChangeUserCart(UserCartDTO info)
        {
            var processId = Guid.NewGuid();
            var userId = info.User.UserId;

            _redisConnection.GetConnection()
                .HashSet($"user:{userId}", info.User.ToHashEntries());

            var userProductsId = _redisConnection.GetConnection()
                .SetMembers($"userCart:{userId}");

            foreach(var product in userProductsId.Where(p => info.Products.All(pr => pr.ProductCatalogId.ToString() != p.ToString())))
            {
                _redisConnection.GetConnection()
                    .SetRemove($"userCart:{userId}", product);
            }

            foreach (var product in info.Products)
            {
                var productKey = $"userCart:{userId}:{product.ProductCatalogId}";
                _redisConnection.GetConnection()
                    .HashSet(productKey, product.ToHashEntries());

                var query = _cassandraConnection.GetConnection()
                    .Prepare(@"INSERT INTO cart (id, moment, userId, imageurl, price, productcatalogid, productname, quantity) 
                                    VALUES (?,?,?,?,?,?,?,?)");

                _cassandraConnection.GetConnection()
                    .Execute(query.Bind(processId, DateTime.Now, info.User.UserId, product.ImageURL, product.Price, 
                        product.ProductCatalogId, product.ProductName, product.Quantity));

                _redisConnection.GetConnection()
                .SetAdd($"userCart:{userId}", new RedisValue(product.ProductCatalogId.ToString()));
            }

            return processId;
        }

        public UserCartDTO GetUserCart(Guid userId)
        {
            var result = new UserCartDTO();

            var userInfo = _redisConnection.GetConnection()
                .HashGetAll($"user:{userId}");

            if (userInfo.Length == 0)
               return null;

            result.AddUserData(userId, userInfo);

            var userProductsId = _redisConnection.GetConnection()
                .SetMembers($"userCart:{userId}");

            foreach (var productCartId in userProductsId)
            {
                var productInfo = _redisConnection.GetConnection()
                    .HashGetAll($"userCart:{userId}:{productCartId}");

                result.AddProductData(Guid.Parse(productCartId.ToString()), productInfo);
            }
          
            return result;
        }

        public List<UserActivityDTO> GetUserActivity(Guid userId)
        {
            var query = _cassandraConnection.GetConnection()
                .Prepare("SELECT * FROM cart WHERE userId = ?");

            var queryResult = _cassandraConnection.GetConnection()
                   .Execute(query.Bind(userId));

            return queryResult.Select(row => new UserActivityDTO(row)).ToList();
        }

        public void RestoreCart(Guid userId, Guid logId)
        {
            var processId = Guid.NewGuid();
            var productsCatalogId = new List<string>();

            var query = _cassandraConnection.GetConnection()
                .Execute(@$"SELECT * FROM cart WHERE id = {logId}");

            foreach (var row in query)
            {
                var product = new ProductCartDTO(row);
                productsCatalogId.Add(product.ProductCatalogId.ToString());

                var productKey = $"userCart:{userId}:{product.ProductCatalogId}";
                _redisConnection.GetConnection()
                    .HashSet(productKey, product.ToHashEntries());

                _redisConnection.GetConnection()
                    .SetAdd($"userCart:{userId}", new RedisValue(product.ProductCatalogId.ToString()));

                var addQuery = _cassandraConnection.GetConnection()
                    .Prepare(@"INSERT INTO cart (id, moment, userId, imageurl, price, productcatalogid, productname, quantity) 
                                        VALUES (?,?,?,?,?,?,?,?)");

                _cassandraConnection.GetConnection()
                    .Execute(addQuery.Bind(processId, DateTime.Now, userId, product.ImageURL, product.Price,
                        product.ProductCatalogId, product.ProductName, product.Quantity));
            }

            var userProductsId = _redisConnection.GetConnection()
                .SetMembers($"userCart:{userId}");

            foreach (var product in userProductsId.Where(id => !productsCatalogId.Contains(id.ToString())))
            {
                _redisConnection.GetConnection()
                    .SetRemove($"userCart:{userId}", product);
            }
        }

        public void EmptyCart(Guid userId)
        {
            var userProductsId = _redisConnection.GetConnection()
                .SetMembers($"userCart:{userId}");

            if (userProductsId == null)
                throw new Exception("Cart is empty");
            
            foreach (var product in userProductsId)
            {
                _redisConnection.GetConnection()
                    .SetRemove($"userCart:{userId}", product);
            }
        }
}