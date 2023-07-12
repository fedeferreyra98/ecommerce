using ecommerce.Commerce.Core.DTOs;
using ecommerce.Models;
namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IProductService
{
    List<Product> GetAllProducts();

    Product GetProductById(Guid id);

    void InsertProduct(ProductDTO productDTO);

    void UpdateProduct(Product product);

    void DeleteProduct(Guid id);
}