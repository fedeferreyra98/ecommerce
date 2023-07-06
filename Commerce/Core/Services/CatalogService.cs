using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }
    public async Task<List<ProductCatalog>> GetAllProductsCatalog()
    {
        return await _catalogRepository.GetAll();
    }

    public async Task<ProductCatalog> GetProductCatalogById(Guid id)
    {
        return await _catalogRepository.GetProductById(id);
    }

    public async Task<List<ProductCatalog>> GetProductCatalogLogById(Guid productId)
    {
        return await _catalogRepository.GetLogByProductId(productId);
    }

    public async Task InsertProductCatalog(ProductCatalogDTO catalogDto)
    {
        var catalog = new ProductCatalog()
        {
            AuthorId = catalogDto.AuthorId,
            ProductId = catalogDto.ProductId,
            Moment = catalogDto.Moment,
            Price = catalogDto.Price
        };
        await _catalogRepository.Insert(catalog);
        await _catalogRepository.InsertProductLog(catalog);
    }

    public async Task UpdateProductCatalog(ProductCatalog catalogUpdated)
    {
        var catalog = await _catalogRepository.GetProductById(catalogUpdated.Id);
        if (catalog is null)
        {
            throw new ArgumentException("El catalogo no existe");
        }
        await _catalogRepository.Update(catalogUpdated);
        await _catalogRepository.InsertProductLog(catalogUpdated);
    }

    public async Task DeleteProductCatalog(Guid id)
    {
        await GetProductCatalogById(id);
        await _catalogRepository.Delete(id);
    }
}