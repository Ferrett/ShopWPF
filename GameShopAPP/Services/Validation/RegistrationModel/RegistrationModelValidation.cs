using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Validation
{
    public class RegistrationModelValidation : IRegistrationModelValidation
    {
        public (bool result, string errorMessage) Validate(RegistrationModel registrationModel)
        {
            var loginValidationResult = ValidateLogin(registrationModel);
            if (loginValidationResult.result == false)
                return (false, loginValidationResult.errorMessage);

            var passwordValidationResult = ValidatePassword(registrationModel);
            if (passwordValidationResult.result == false)
                return (false, passwordValidationResult.errorMessage);

            var nicknameValidationResult = ValidateNickname(registrationModel);
            if (nicknameValidationResult.result == false)
                return (false, nicknameValidationResult.errorMessage);

            if (!string.IsNullOrEmpty(registrationModel.email))
            {
                var emailValidationResult = ValidateEmail(registrationModel);
                if (emailValidationResult.result == false)
                    return (false, emailValidationResult.errorMessage);
            }

            return (true, string.Empty);
        }

        private const int MinLoginLength = 5;
        private const int MaxLoginLength = 99;
        private (bool result, string errorMessage) ValidateLogin(RegistrationModel registrationModel)
        {
            if (registrationModel.login.Length < MinLoginLength)
            {
                return (false, $"Login is too short");
            }

            if (registrationModel.login.Length > MaxLoginLength)
            {
                return (false, $"Login is too long");
            }

            if (IsAllLettersOrDigits(registrationModel.login) == false)
            {
                return (false, $"Login can contain only latin letters or digits");
            }

            return (true, string.Empty);
        }

        private const int MinPasswordLength = 7;
        private const int MaxPasswordLength = 256;
        private (bool result, string errorMessage) ValidatePassword(RegistrationModel registrationModel)
        {
            if (registrationModel.password.Length < MinPasswordLength)
            {
                return (false, $"Password is too short");
            }

            if (registrationModel.password.Length > MaxPasswordLength)
            {
                return (false, $"Password is too long");
            }

            return (true, string.Empty);
        }

        private const int MinNicknameLength = 1;
        private const int MaxNicknameLength = 30;
        private (bool result, string errorMessage) ValidateNickname(RegistrationModel registrationModel)
        {
            if (registrationModel.nickname.Length < MinNicknameLength)
            {
                return (false, $"Nickname is too short");
            }

            if (registrationModel.nickname.Length > MaxNicknameLength)
            {
                return (false, $"Nickname is too long");
            }

            return (true, string.Empty);
        }

        private const int MaxEmailLength = 256;
        private (bool result, string errorMessage) ValidateEmail(RegistrationModel registrationModel)
        {
            if (new EmailAddressAttribute().IsValid(registrationModel.email) == false)
            {
                return (false, $"Email is not valid");
            }

            if (IsAllLettersOrDigits(registrationModel.email!.Replace("@", "").Replace(".","")) == false)
            {
                return (false, $"Email can contain only latin letters or digits");
            }

            if (registrationModel.email.Length > MaxEmailLength)
            {
                return (false, $"Email is too long");
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
