using ecommerce.Cart.Core.Dtos;

namespace ecommerce.Cart.Core.Repositories.Interfaces;

public interface IUserCartRepository
{
    Task<Guid> ChangeUserCartAsync(UserCartDTO userCartInfo);

    Task<List<UserActivityDTO>> GetUserActivityAsync(Guid userId);

    Task<UserCartDTO?> GetUserCartAsync(Guid userId);

    Task RestoreCart(Guid userId, Guid logId);

    Task EmptyCart(Guid userId);
}