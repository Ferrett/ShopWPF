using GameShopAPP.Models;

namespace GameShopAPP.Services.Validation
{
    public interface IDeveloperValidation
    {
        (bool result, string errorMessage) Validate(Developer developer);
    }
}
