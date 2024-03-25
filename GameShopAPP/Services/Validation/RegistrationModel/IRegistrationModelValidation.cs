using GameShopAPP.Models.ServiceModels;

namespace GameShopAPP.Services.Validation
{
    public interface IRegistrationModelValidation
    {
        (bool result, string errorMessage) Validate(RegistrationModel registrationModel);
    }
}
