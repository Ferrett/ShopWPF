using GameShopAPP.Models;

namespace GameShopAPP.Services.Validation
{
    public class ReviewValidation : IReviewValidation
    {
        public (bool result, string errorMessage) Validate(Review review)
        {
            if (!string.IsNullOrEmpty(review.text))
            {
                var textValidationResult = ValidateText(review);
                if (textValidationResult.result == false)
                    return (false, textValidationResult.errorMessage);
            }

            return (true, string.Empty);
        }

        private const int MaxTextLength = 9999;
        public (bool result, string errorMessage) ValidateText(Review review)
        {
            if (review.text!.Length > MaxTextLength)
            {
                return (false, $"Text is too long. Make it {review.text!.Length-MaxTextLength} symbols shorter");
            }

            return (true, string.Empty);
        }
    }
}
