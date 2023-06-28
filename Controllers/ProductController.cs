using ecommerce.Abstractions;
using ecommerce.Models;
using ecommerce.Services;

namespace ecommerce.Controllers;

public class ProductController : IController<Product>
{
    private ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }
    public Product Get(string id)
    {
        return _service.Get(id);
    }

    public void Save(Product product)
    {
        _service.Save(product);
    }

    public void Print(Product entity)
    {
        throw new NotImplementedException();
    }
}