using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Validation
{
    public interface IUserValidation
    {
        (bool result, string errorMessage) Validate(User user);
    }
}
