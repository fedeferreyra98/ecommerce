using ecommerce.Cart.Core.Dtos;

namespace ecommerce.Cart.Core.Services.Interfaces;

public interface IUserCartService
{
    
    Task<UserCartDTO?> GetUserCartAsync(Guid userId);

    Task<List<UserActivityDTO>> GetUserActivityAsync(Guid userId);

    Task<Guid> ChangeUserCart(UserCartDTO userCartInfo);

    Task RestoreCart(Guid userId, Guid logId);

    Task Checkout(Guid userId);
}