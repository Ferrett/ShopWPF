using GameShopAPP.Models;

namespace GameShopAPP.Services.Validation
{
    public interface IGameValidation
    {
        (bool result, string errorMessage) Validate(Game game);
    }
}
