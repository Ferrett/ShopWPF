using GameShopAPP.Models;

namespace GameShopAPP.Services.Validation
{
    public interface IReviewValidation
    {
        (bool result, string errorMessage) Validate(Review review);
    }
}
