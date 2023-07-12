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
    public List<Order> GetAllOrders()
    {
        return _orderRepository.GetAll();
    }

    public Order GetOrderById(Guid id)
    {
        var order = _orderRepository.GetById(id);

        if (order is null) throw new ApplicationException("El pedido no existe.");

        return order;
    }

    public void InsertOrder(OrderDTO order)
    {
        var user = _userService.GetUserById(order.UserId);
        var orderPrice = 0m;
        var cartProducts = new List<ProductCart>();

        VerifyStock(order);
        
        foreach (var productOrdered in order.Products)
        {
            var productCatalog = _catalogService.GetProductCatalogById(productOrdered.ProductCatalogId);
            var productInStock = _productRepository.GetById(productCatalog.ProductId);
            productInStock.Stock -= productOrdered.Quantity;
            _productRepository.Update(productInStock);
            
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

        _orderRepository.Insert(newOrder);
    }

    private void VerifyStock(OrderDTO order)
    {
        foreach (var productOrdered in order.Products)
        {
            var productCatalog = _catalogService.GetProductCatalogById(productOrdered.ProductCatalogId);
            var productInStock = _productRepository.GetById(productCatalog.ProductId);

            if (productInStock.Stock < productOrdered.Quantity)
                throw new ApplicationException("No hay stock suficiente");
        }
    }

    public void DeleteOrder(Guid id)
    {
        _orderRepository.Delete(id);
    }

    public List<Order> GetOrderByStatus(bool status)
    { 
        return _orderRepository.GetAllByStatus(status);
    }

    public void ChangeStatus(Order order)
    {
        _orderRepository.Update(order);
    }
}