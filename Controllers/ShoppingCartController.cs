using ecommerce.Abstractions;
using ecommerce.Models;
using ecommerce.Services;

namespace ecommerce.Controllers;

public class ShoppingCartController : IController<ShoppingCart>
{
    private ShoppingCartService _service;

    public ShoppingCartController(ShoppingCartService service)
    {
        _service = service;
    }

    public ShoppingCart Get(string id)
    {
        return _service.Get(id);
    }
    
    public void Save(ShoppingCart cart)
    {
        _service.Save(cart);
    }

    public void Print(ShoppingCart cart)
    {
        //TODO: Renderizar el carrito en la consola
    }

    public void AddProduct(string cartId, string productId)
    {
        _service.AddProduct(cartId, productId);
    }
    
    public void RemoveProduct(string cartId, string productId)
    {
        _service.RemoveProduct(cartId, productId);
    }
}