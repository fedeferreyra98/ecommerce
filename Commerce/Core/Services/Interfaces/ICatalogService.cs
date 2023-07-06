using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface ICatalogService
{
    Task<List<ProductCatalog>> GetAllProductsCatalog();

    Task<ProductCatalog> GetProductCatalogById(Guid id);

    Task<List<ProductCatalog>> GetProductCatalogLogById(Guid productId);

    Task InsertProductCatalog(ProductCatalogDTO catalogDto);

    Task UpdateProductCatalog(ProductCatalog catalogUpdated);

    Task DeleteProductCatalog(Guid id);
}
