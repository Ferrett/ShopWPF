using GameShopAPP.Model;
using GameShopAPP.Model.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Validation.DeveloperValidation
{
    public interface IDeveloperValidation
    {
        (bool result, string errorMessage) Validate(Developer developer);
    }
}
