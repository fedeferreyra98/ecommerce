using System.Net.Mime;
using System.Text;
using ecommerce.Cart.Core.Dtos;
using ecommerce.Cart.Core.Repositories.Interfaces;
using ecommerce.Cart.Core.Services.Interfaces;
using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services.Interfaces;
using ProductCartDTO = ecommerce.Commerce.Core.DTOs.ProductCartDTO;

namespace ecommerce.Cart.Core.Services;

public class UserCartService : IUserCartService
{
    private readonly IUserCartRepository _userCartRepository;
    private readonly IOrderService _orderService;

    public UserCartService(IUserCartRepository userCartRepository, IOrderService orderService)
    {
        _userCartRepository = userCartRepository;
        _orderService = orderService;
    }
    public async Task<UserCartDTO?> GetUserCartAsync(Guid userId)
    {
        return await _userCartRepository.GetUserCartAsync(userId);
    }

    public async Task<List<UserActivityDTO>> GetUserActivityAsync(Guid userId)
    {
        return await _userCartRepository.GetUserActivityAsync(userId);
    }

    public async Task<Guid> ChangeUserCart(UserCartDTO userCartInfo)
    {
        return await _userCartRepository.ChangeUserCartAsync(userCartInfo);
    }

    public async Task RestoreCart(Guid userId, Guid logId)
    {
        await _userCartRepository.RestoreCart(userId, logId);
    }

    public async Task Checkout(Guid userId)
    {
        var checkoutInfo = await _userCartRepository.GetUserCartAsync(userId);
        if (checkoutInfo == null)
            throw new Exception("Empty cart");

        var userCheckoutId = checkoutInfo.User.UserId;
        var products = checkoutInfo.Products.Select(p => new ProductCartDTO()
        {
            ProductCatalogId = p.ProductCatalogId,
            Quantity = p.Quantity
        });
        var order = new OrderDTO()
        {
            UserId = userCheckoutId,
            Products = products.ToList(),
            IVA = true
        };

        await _orderService.InsertOrder(order);
        await _userCartRepository.EmptyCart(userId);
    }
}