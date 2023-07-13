using ecommerce.Cart.Core.Dtos;

namespace ecommerce.Cart.Core.Repositories.Interfaces;

public interface IUserCartRepository
{
    Guid ChangeUserCart(UserCartDTO userCartInfo);

    List<UserActivityDTO> GetUserActivity(Guid userId);

    UserCartDTO GetUserCart(Guid userId);
    void CreateCart(UserCartDTO userCartDto);

    void RestoreCart(Guid userId, Guid logId);

    void EmptyCart(Guid userId);
}