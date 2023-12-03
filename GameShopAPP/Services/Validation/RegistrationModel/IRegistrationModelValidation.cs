using GameShopAPP.Model.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Validation.RegistrationValidation
{
    public interface IRegistrationModelValidation
    {
        (bool result, string errorMessage) Validate(RegistrationModel registrationModel);
    }
}
