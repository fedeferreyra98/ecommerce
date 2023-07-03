using ecommerce.Abstractions;
using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Repositories;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services.Interfaces;
using ecommerce.Models;

namespace ecommerce.Commerce.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _productRepository.GetAll();
    }

    public async Task<Product> GetProductById(Guid id)
    {
        var product = await _productRepository.GetById(id);
        
        if (product is null) throw new ApplicationException("El producto no existe");

        return product;
    }

    public async Task InsertProduct(ProductDTO productDTO)
    {
        var product = new Product()
        {
            ProductName = productDTO.ProductName,
            ImagesURL = productDTO.ImagesURL,
            MainImage = productDTO.MainImage,
            Description = productDTO.Description,
            Comments = productDTO.Comments,
            Stock = productDTO.Stock
        };
        await _productRepository.Insert(product);
    }

    public async Task UpdateProduct(Product product)
    {
        await _productRepository.Update(product);
    }

    public async Task DeleteProduct(Guid id)
    {
        await _productRepository.Delete(id);
    }
}