using GameShopAPP.Models;

namespace GameShopAPP.Services.Validation
{
    public class GameValidation : IGameValidation
    {
        public (bool result, string errorMessage) Validate(Game game)
        {
            var titleValidationResult = ValidateTitle(game);
            if (titleValidationResult.result == false)
                return (false, titleValidationResult.errorMessage);

            var priceValidationResult = ValidatePrice(game);
            if (priceValidationResult.result == false)
                return (false, priceValidationResult.errorMessage);

            var achievementsAmountValidationResult = ValidatePrice(game);
            if (achievementsAmountValidationResult.result == false)
                return (false, achievementsAmountValidationResult.errorMessage);

            return (true, string.Empty);
        }

        private const int MinTitleLength = 1;
        private const int MaxTitleLength = 256;
        public (bool result, string errorMessage) ValidateTitle(Game game)
        {
            if (game.title.Length < MinTitleLength)
            {
                return (false, $"Title is too short");
            }

            if (game.title.Length > MaxTitleLength)
            {
                return (false, $"Title is too long");
            }

            return (true, string.Empty);
        }

        private const float MinPrice = 0.0f;
        private const float MaxPrice = 9999.0f;
        public (bool result, string errorMessage) ValidatePrice(Game game)
        {
            if (game.priceUSD < MinPrice)
            {
                return (false, $"Price can't be negative");
            }

            if (game.priceUSD > MaxPrice)
            {
                return (false, $"Price is too high");
            }

            return (true, string.Empty);
        }

        private const int MinAchievementsAmount = 0;
        private const int MaxAchievementsAmount = 9999;
        public (bool result, string errorMessage) ValidateAchievementsAmount(Game game)
        {
            if (game.achievementsAmount < MinAchievementsAmount)
            {
                return (false, $"Achievements amount can't be negative");
            }

            if (game.achievementsAmount > MaxAchievementsAmount)
            {
                return (false, $"Achievements amount is too high");
            }

            return (true, string.Empty);
        }
    }
}
