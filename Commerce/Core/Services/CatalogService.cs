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
    public List<ProductCatalog> GetAllProductsCatalog()
    {
        return _catalogRepository.GetAll();
    }

    public ProductCatalog GetProductCatalogById(Guid id)
    {
        return _catalogRepository.GetProductById(id);
    }

    public List<ProductCatalog> GetProductCatalogLogById(Guid productId)
    {
        return _catalogRepository.GetLogByProductId(productId);
    }

    public void InsertProductCatalog(ProductCatalogDTO catalogDto)
    {
        var catalog = new ProductCatalog()
        {
            AuthorId = catalogDto.AuthorId,
            ProductId = catalogDto.ProductId,
            Moment = catalogDto.Moment,
            Price = catalogDto.Price
        };
        _catalogRepository.Insert(catalog);
        _catalogRepository.InsertProductLog(catalog);
    }

    public void UpdateProductCatalog(ProductCatalog catalogUpdated)
    {
        var catalog = _catalogRepository.GetProductById(catalogUpdated.Id);
        if (catalog is null)
        {
            throw new ArgumentException("El catalogo no existe");
        }
        _catalogRepository.Update(catalogUpdated);
        _catalogRepository.InsertProductLog(catalogUpdated);
    }

    public void DeleteProductCatalog(Guid id)
    {
        GetProductCatalogById(id);
        _catalogRepository.Delete(id);
    }
}