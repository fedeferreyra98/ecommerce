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

    public async Task<UserCartDTO?> GetUserCart(Guid userId)
    {
        return await _userCartService.GetUserCartAsync(userId);
    }

    public async Task<List<UserActivityDTO>> GetUserActivity(Guid userId)
    {
        return await _userCartService.GetUserActivityAsync(userId);
    }

    public async Task ChangeUserCart(UserCartDTO userCartDto)
    {
        await _userCartService.ChangeUserCart(userCartDto);
    }

    public async Task RestoreCart(Guid userId, Guid logId)
    {
        await _userCartService.RestoreCart(userId, logId);
    }

    public async Task Checkout(Guid userId)
    {
        await _userCartService.Checkout(userId);
    }
}