using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface ICatalogRepository
{
    List<ProductCatalog> GetAll();

    ProductCatalog GetProductById(Guid id);

    List<ProductCatalog> GetLogByProductId(Guid productId);

    void Insert(ProductCatalog catalog);

    void InsertProductLog(ProductCatalog catalog);

    void Update(ProductCatalog catalog);

    void Delete(Guid productCatalogId);
}
