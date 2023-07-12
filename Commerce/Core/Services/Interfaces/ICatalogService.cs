using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface ICatalogService
{
    List<ProductCatalog> GetAllProductsCatalog();

    ProductCatalog GetProductCatalogById(Guid id);

    List<ProductCatalog> GetProductCatalogLogById(Guid productId);

    void InsertProductCatalog(ProductCatalogDTO catalogDto);

    void UpdateProductCatalog(ProductCatalog catalogUpdated);

    void DeleteProductCatalog(Guid id);
}
