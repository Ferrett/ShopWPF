using GameShopAPP.Model;
using GameShopAPP.Model.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Validation.UserValidation
{
    public interface IUserValidation
    {
        (bool result, string errorMessage) Validate(User user);
    }
}
