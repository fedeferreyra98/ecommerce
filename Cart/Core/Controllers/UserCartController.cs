using ecommerce.Cart.Core.Dtos;
using ecommerce.Cart.Core.Services.Interfaces;

namespace ecommerce.Cart.Core.Controllers;

public class UserCartController
{
    private readonly IUserCartService? _userCartService;

    public UserCartController(IUserCartService? userCartService)
    {
        _userCartService = userCartService;
    }

    public UserCartDTO GetUserCart(Guid userId)
    {
        return _userCartService.GetUserCart(userId);
    }

    public List<UserActivityDTO> GetUserActivity(Guid userId)
    {
        return _userCartService.GetUserActivity(userId);
    }

    public Guid ChangeUserCart(UserCartDTO userCartDto)
    {
        return _userCartService.ChangeUserCart(userCartDto);
    }

    public void RestoreCart(Guid userId, Guid logId)
    {
        _userCartService.RestoreCart(userId, logId);
    }

    public void Checkout(Guid userId)
    {
        _userCartService.Checkout(userId);
    }
}