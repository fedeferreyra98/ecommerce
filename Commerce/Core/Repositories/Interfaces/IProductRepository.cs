using ecommerce.Models;

namespace ecommerce.Commerce.Core.Repositories.Interfaces;

public interface IProductRepository
{
    List<Product> GetAll();

    Product GetById(Guid id);

    void Insert(Product product);

    void Update(Product product);

    void Delete(Guid product);
}