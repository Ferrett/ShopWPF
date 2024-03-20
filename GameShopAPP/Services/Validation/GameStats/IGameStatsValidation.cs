using GameShopAPP.Models;

namespace GameShopAPP.Services.Validation
{
    public interface IGameStatsValidation
    {
        (bool result, string errorMessage) Validate(GameStats gameStats);
    }
}
