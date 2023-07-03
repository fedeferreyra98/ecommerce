using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class CatalogController
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<List<ProductCatalog>> GetCatalog()
    {
        return await _catalogService.GetAllProductsCatalog();
    }

    public async Task<ProductCatalog> GetProductCatalogById(Guid id)
    {
        return await _catalogService.GetProductCatalogById(id);
    }

    public async Task<List<ProductCatalog>> GetCatalogLogById(Guid id)
    {
        return await _catalogService.GetProductCatalogLogById(id);
    }

    public async void InsertProductCatalog(ProductCatalogDTO productCatalogDto)
    {
        if (productCatalogDto is null)
            throw new ArgumentNullException(nameof(productCatalogDto), "Debe ingresar un producto de catalogo.");
        await _catalogService.InsertProductCatalog(productCatalogDto);
    }

    public async void UpdateProductCatalog(ProductCatalog productCatalog)
    {
        if (productCatalog is null)
            throw new ArgumentNullException(nameof(productCatalog), "Debe ingresar un producto de catalogo.");
        await _catalogService.UpdateProductCatalog(productCatalog);
    }

    public async void DeleteProductCatalog(Guid id)
    {
        await _catalogService.DeleteProductCatalog(id);
    }
    
}