using GameShopAPP.Models.ServiceModels;

namespace GameShopAPP.Services.Validation
{
    public interface ILoginModelValidation
    {
        (bool result, string errorMessage) Validate(LoginModel loginModel);
    }
}
