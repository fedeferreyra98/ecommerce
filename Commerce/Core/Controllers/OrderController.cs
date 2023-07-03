using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Services.Interfaces;

namespace ecommerce.Commerce.Core.Controllers;

public class OrderController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<Order> GetById(Guid id)
    {
        return await _orderService.GetOrderById(id);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _orderService.GetAllOrders();
    }

    public async Task<List<Order>> GetOrderByStatus(bool status)
    {
        return await _orderService.GetOrderByStatus(status);
    }

    public async void CreateOrder(OrderDTO order)
    {
        await _orderService.InsertOrder(order); 
        Console.WriteLine("Orden creada con exito!");
    }

    public async void DeleteOrder(Guid id)
    {
        await _orderService.DeleteOrder(id);
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