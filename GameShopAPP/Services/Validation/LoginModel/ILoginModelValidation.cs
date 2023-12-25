using GameShopAPP.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Validation
{
    public interface ILoginModelValidation
    {
        (bool result, string errorMessage) Validate(LoginModel loginModel);
    }
}
