using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Services.Interfaces;
using ecommerce.Models;

namespace ecommerce.Commerce.Core.Controllers;

public class ProductController
{
    private readonly IProductService? _service;

    public ProductController(IProductService? service)
    {
        _service = service;
    }
    public async Task<Product> GetById(Guid id)
    {
        return await _service.GetProductById(id);
    }
    
    public async Task<List<Product>> GetAll()
    {
        return await _service.GetAllProducts();
    }

    public async void Create(ProductDTO productDto)
    {
        await _service.InsertProduct(productDto);
    }

    public async void Update(Product product)
    {
        await _service.UpdateProduct(product);
    }

    public void Print(Product product)
    {
        throw new NotImplementedException();
    }
}