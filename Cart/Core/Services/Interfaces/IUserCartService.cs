using ecommerce.Cart.Core.Dtos;

namespace ecommerce.Cart.Core.Services.Interfaces;

public interface IUserCartService
{
    UserCartDTO GetUserCart(Guid userId);

    List<UserActivityDTO> GetUserActivity(Guid userId);

    Guid ChangeUserCart(UserCartDTO userCartInfo);

    void RestoreCart(Guid userId, Guid logId);

    void Checkout(Guid userId);
    void CreateCart(UserCartDTO userCartDto);
}