using ecommerce.Abstractions;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Services;

public class ShoppingCartService : IService<ShoppingCart>
{
    private readonly ShoppingCartRepository _repository;

    public ShoppingCartService(ShoppingCartRepository repository)
    {
        _repository = repository;
    }

    public ShoppingCart Get(string id)
    {
        return _repository.Get(id);
    }

    public void Save(ShoppingCart cart)
    {
        _repository.Save(cart);
    }

    public void AddProduct(string cartId, string productId)
    {
        var cart = _repository.Get(cartId);
        if (cart.Products.ContainsKey(productId))
        {
            cart.Products[productId]++;
        }
        else
        {
            cart.Products.Add(productId, 1);
        }
        _repository.Save(cart);
    }
    
    public void RemoveProduct(string cartId, string productId)
    {
        var cart = _repository.Get(cartId);
        if (!cart.Products.ContainsKey(productId)) return;
        
        cart.Products[productId]--;
        if (cart.Products[productId] <= 0)
        {
            cart.Products.Remove(productId);
        }
        _repository.Save(cart);
    }
}