using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserService _userService;
    private readonly ICatalogService _catalogService;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository,
        IUserService userService, ICatalogService catalogService)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userService = userService;
        _catalogService = catalogService;
    }
    public async Task<List<Order>> GetAllOrders()
    {
        return await _orderRepository.GetAll();
    }

    public async Task<Order> GetOrderById(Guid id)
    {
        var order = await _orderRepository.GetById(id);

        if (order is null) throw new ApplicationException("El pedido no existe.");

        return order;
    }

    public async Task InsertOrder(OrderDTO order)
    {
        var user = await _userService.GetUserById(order.UserId);
        var orderPrice = 0m;
        var cartProducts = new List<ProductCart>();

        VerifyStock(order);
        
        foreach (var productOrdered in order.Products)
        {
            var productCatalog = _catalogService.GetProductCatalogById(productOrdered.ProductCatalogId);
            var productInStock = await _productRepository.GetById(productCatalog.ProductId);
            productInStock.Stock -= productOrdered.Quantity;
            await _productRepository.Update(productInStock);
            
            //Add the products
            cartProducts.Add(new ProductCart(){ProductCatalog = productCatalog, Quantity = productOrdered.Quantity});
            
            //Calculate price
            orderPrice += productCatalog.Price * productOrdered.Quantity;
        }

        var newOrder = new Order()
        {
            FinalPrice = orderPrice,
            IVA = order.IVA,
            User = user,
            Products = cartProducts,
            TimeStamp = DateTime.Now
        };

        await _orderRepository.Insert(newOrder);
    }

    private async void VerifyStock(OrderDTO order)
    {
        foreach (var productOrdered in order.Products)
        {
            var productCatalog = _catalogService.GetProductCatalogById(productOrdered.ProductCatalogId);
            var productInStock = await _productRepository.GetById(productCatalog.ProductId);

            if (productInStock.Stock < productOrdered.Quantity)
                throw new ApplicationException("No hay stock suficiente");
        }
    }

    public async Task DeleteOrder(Guid id)
    {
        await _orderRepository.Delete(id);
    }

    public async Task<List<Order>> GetOrderByStatus(bool status)
    { 
        return await _orderRepository.GetAllByStatus(status);
    }

    public async Task ChangeStatus(Order order)
    {
        await _orderRepository.Update(order);
    }
}