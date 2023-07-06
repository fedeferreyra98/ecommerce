using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface ICatalogRepository
{
    Task<List<ProductCatalog>> GetAll();

    Task<ProductCatalog> GetProductById(Guid id);

    Task<List<ProductCatalog>> GetLogByProductId(Guid productId);

    Task Insert(ProductCatalog catalog);

    Task InsertProductLog(ProductCatalog catalog);

    Task Update(ProductCatalog catalog);

    Task Delete(Guid productCatalogId);
}
