using System.Runtime.CompilerServices;
using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class CatalogController
{
    private readonly ICatalogService? _catalogService;

    public CatalogController(ICatalogService? catalogService)
    {
        _catalogService = catalogService;
    }

    public  List<ProductCatalog> GetCatalog()
    {
        return  _catalogService.GetAllProductsCatalog();
    }

    public  ProductCatalog GetProductCatalogById(Guid id)
    {
        return  _catalogService.GetProductCatalogById(id);
    }

    public  List<ProductCatalog> GetCatalogLogById(Guid productId)
    {
        return  _catalogService.GetProductCatalogLogById(productId);
    }

    public void InsertProductCatalog(ProductCatalogDTO productCatalogDto)
    {
        if (productCatalogDto is null)
            throw new ArgumentNullException(nameof(productCatalogDto), "Debe ingresar un producto de catalogo.");
        _catalogService.InsertProductCatalog(productCatalogDto);
    }

    public void UpdateProductCatalog(ProductCatalog productCatalog)
    {
        if (productCatalog is null)
            throw new ArgumentNullException(nameof(productCatalog), "Debe ingresar un producto de catalogo."); 
        _catalogService.UpdateProductCatalog(productCatalog);
    }

    public void DeleteProductCatalog(Guid id)
    { 
        _catalogService.DeleteProductCatalog(id);
    }
}