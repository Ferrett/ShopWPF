using GameShopAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Validation.GameStatsValidation
{
    public class GameStatsValidation : IGameStatsValidation
    {
        public (bool result, string errorMessage) Validate(GameStats gameStats)
        {
            var hoursPlayedValidationResult = ValidateHoursPlayed(gameStats);
            if (hoursPlayedValidationResult.result == false)
                return (false, hoursPlayedValidationResult.errorMessage);

            var achievementsGottenPlayedValidationResult = ValidateAchievementsGotten(gameStats);
            if (achievementsGottenPlayedValidationResult.result == false)
                return (false, achievementsGottenPlayedValidationResult.errorMessage);

            return (true, string.Empty);
        }

        private const float MinHoursPlayed = 0.0f;
        public (bool result, string errorMessage) ValidateHoursPlayed(GameStats gameStats)
        {
            if (gameStats.hoursPlayed < MinHoursPlayed)
            {
                return (false, $"Amount of played hours can't be negative");
            }

            return (true, string.Empty);
        }

        private const float MinAchievementsGotten = 0.0f;
        private const float MaxAchievementsGotten = 9999.0f;
        public (bool result, string errorMessage) ValidateAchievementsGotten(GameStats gameStats)
        {
            if (gameStats.achievementsGotten < MinAchievementsGotten)
            {
                return (false, $"Amount of gotten achievements can't be negative");
            }

            if (gameStats.achievementsGotten > MaxAchievementsGotten)
            {
                return (false, $"Amount of gotten achievements can't higher than {MaxAchievementsGotten}");
            }

            return (true, string.Empty);
        }
    }
}
