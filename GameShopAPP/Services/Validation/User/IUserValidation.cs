using GameShopAPP.Models;

namespace GameShopAPP.Services.Validation
{
    public interface IUserValidation
    {
        (bool result, string errorMessage) Validate(User user);
    }
}
