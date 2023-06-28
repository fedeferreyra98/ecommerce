using ecommerce.Abstractions;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Services;

public class ProductService : IService<Product>
{
    private ProductRepository _repository;

    public ProductService(ProductRepository repository )
    {
        _repository = repository;
    } 
    public Product Get(string id)
    {
        return _repository.Get(id);
    }

    public void Save(Product product)
    {
        _repository.Save(product);
    }
}