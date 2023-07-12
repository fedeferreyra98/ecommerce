using ecommerce.Commerce.Core.DTOs;
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

    public List<Product> GetAllProducts()
    {
        return _productRepository.GetAll();
    }

    public Product GetProductById(Guid id)
    {
        var product = _productRepository.GetById(id);
        
        if (product is null) throw new ApplicationException("El producto no existe");

        return product;
    }

    public void InsertProduct(ProductDTO productDTO)
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
        _productRepository.Insert(product);
    }

    public void UpdateProduct(Product product)
    {
        _productRepository.Update(product);
    }

    public void DeleteProduct(Guid id)
    {
        _productRepository.Delete(id);
    }
}