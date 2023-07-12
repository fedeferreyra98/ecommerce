using ecommerce.Cart.Core.Dtos;
using ecommerce.Cart.Core.Repositories.Interfaces;
using ecommerce.Cart.Core.Services.Interfaces;
using ecommerce.Commerce.Core.DTOs;
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
    public UserCartDTO GetUserCart(Guid userId)
    {
        return _userCartRepository.GetUserCart(userId);
    }

    public List<UserActivityDTO> GetUserActivity(Guid userId)
    {
        return _userCartRepository.GetUserActivity(userId);
    }

    public Guid ChangeUserCart(UserCartDTO userCartInfo)
    {
        return _userCartRepository.ChangeUserCart(userCartInfo);
    }

    public void RestoreCart(Guid userId, Guid logId)
    {
        _userCartRepository.RestoreCart(userId, logId);
    }

    public void Checkout(Guid userId)
    {
        var checkoutInfo = _userCartRepository.GetUserCart(userId);
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

        _orderService.InsertOrder(order);
        _userCartRepository.EmptyCart(userId);
    }
}