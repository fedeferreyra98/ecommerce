using ecommerce.Commerce.Core.DTOs;
using ecommerce.Models;

namespace ecommerce.Commerce.Core.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();

    Task<Product> GetProductById(Guid id);

    Task InsertProduct(ProductDTO productDTO);

    Task UpdateProduct(Product product);

    Task DeleteProduct(Guid id);
}