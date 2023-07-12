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
    public Product GetById(Guid id)
    {
        return _service.GetProductById(id);
    }
    
    public List<Product> GetAll()
    {
        return _service.GetAllProducts();
    }

    public void Create(ProductDTO productDto)
    {
        _service.InsertProduct(productDto);
    }

    public void Update(Product product)
    {
        _service.UpdateProduct(product);
    }

    public void Print(Product product)
    {
        throw new NotImplementedException();
    }
}