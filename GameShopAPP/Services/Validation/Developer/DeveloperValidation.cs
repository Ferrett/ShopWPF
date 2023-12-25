using GameShopAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Validation
{
    public class DeveloperValidation : IDeveloperValidation
    {
        public (bool result, string errorMessage) Validate(Developer developer)
        {
            var titleValidationResult = ValidateName(developer);
            if (titleValidationResult.result == false)
                return (false, titleValidationResult.errorMessage);

            return (true, string.Empty);
        }

        private const int MinNameLength = 1;
        private const int MixNameLength = 256;
        public (bool result, string errorMessage) ValidateName(Developer developer)
        {
            if (developer.name.Length < MinNameLength)
            {
                return (false, $"Name is too short");
            }

            if (developer.name.Length > MixNameLength)
            {
                return (false, $"Name is too long");
            }

            if (IsAllLettersOrDigits(developer.name) == false)
            {
                return (false, $"Login can contain only latin letters or digits");
            }

            return (true, string.Empty);
        }

        private bool IsAllLettersOrDigits(string str)
        {
            foreach (char c in str)
            {
                if ((c >= 'a' && c <= 'z') == false && (c >= 'A' && c <= 'Z') == false && (c >= '0' && c <= '9') == false)
                    return false;
            }
            return true;
        }
    }
}
