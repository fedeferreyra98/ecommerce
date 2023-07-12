using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class OrderController
{
    private readonly IOrderService? _orderService;

    public OrderController(IOrderService? orderService)
    {
        _orderService = orderService;
    }
    public Order GetById(Guid id)
    {
        return _orderService.GetOrderById(id);
    }

    public List<Order> GetAllOrders()
    {
        return _orderService.GetAllOrders();
    }

    public List<Order> GetOrderByStatus(bool status)
    {
        return _orderService.GetOrderByStatus(status);
    }

    public void CreateOrder(OrderDTO order)
    {
        _orderService.InsertOrder(order); 
        Console.WriteLine("Orden creada con exito!");
    }

    public void DeleteOrder(Guid id)
    {
        _orderService.DeleteOrder(id);
        Console.WriteLine("Orden eliminada con exito!");
    }
    
    public void Print(Order order)
    {
        Console.WriteLine();
        Console.WriteLine($"Nro de orden: {order.OrderId}");
        Console.WriteLine($"Estado: {order.OrderStatus}");
        PrintProducts(order);
        Console.WriteLine($"Fecha: {order.TimeStamp}");
        Console.WriteLine($"IVA: {order.IVA}");
        Console.WriteLine($"Precio final: {order.FinalPrice}");
        Console.WriteLine();
    }

    private void PrintProducts(Order order)
    {
        foreach (var product in order.Products)
        {
            Console.WriteLine($"Id Producto: {product.ProductCatalog.ProductId}   Cantidad: {product.Quantity.ToString()}");
        }
    }
}