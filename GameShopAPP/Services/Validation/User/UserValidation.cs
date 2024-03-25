using GameShopAPP.Models;
using System.ComponentModel.DataAnnotations;

namespace GameShopAPP.Services.Validation
{
    public class UserValidation : IUserValidation
    {
        public (bool result, string errorMessage) Validate(User user)
        {
            var loginValidationResult = ValidateLogin(user);
            if (loginValidationResult.result == false)
                return (false, loginValidationResult.errorMessage);

            var passwordValidationResult = ValidatePassword(user);
            if (passwordValidationResult.result == false)
                return (false, passwordValidationResult.errorMessage);

            var nicknameValidationResult = ValidateNickname(user);
            if (nicknameValidationResult.result == false)
                return (false, nicknameValidationResult.errorMessage);

            if (!string.IsNullOrEmpty(user.email))
            {
                var emailValidationResult = ValidateEmail(user);
                if (emailValidationResult.result == false)
                    return (false, emailValidationResult.errorMessage);
            }

            return (true, string.Empty);
        }

        private const int MinLoginLength = 5;
        private const int MaxLoginLength = 99;
        private (bool result, string errorMessage) ValidateLogin(User user)
        {
            if (user.login.Length < MinLoginLength)
            {
                return (false, $"Login is too short");
            }

            if (user.login.Length > MaxLoginLength)
            {
                return (false, $"Login is too long");
            }

            if (IsAllLettersOrDigits(user.login) == false)
            {
                return (false, $"Login can contain only latin letters or digits");
            }

            return (true, string.Empty);
        }

        private const int MinPasswordLength = 7;
        private const int MaxPasswordLength = 256;
        private (bool result, string errorMessage) ValidatePassword(User user)
        {
            if (user.password.Length < MinPasswordLength)
            {
                return (false, $"Password is too short");
            }

            if (user.password.Length > MaxPasswordLength)
            {
                return (false, $"Password is too long");
            }

            return (true, string.Empty);
        }

        private const int MinNicknameLength = 1;
        private const int MaxNicknameLength = 30;
        private (bool result, string errorMessage) ValidateNickname(User user)
        {
            if (user.nickname.Length < MinNicknameLength)
            {
                return (false, $"Nickname is too short");
            }

            if (user.nickname.Length > MaxNicknameLength)
            {
                return (false, $"Nickname is too long");
            }

            return (true, string.Empty);
        }

        private const int MaxEmailLength = 256;
        private (bool result, string errorMessage) ValidateEmail(User user)
        {
            if (new EmailAddressAttribute().IsValid(user.email) == false)
            {
                return (false, $"Email is not valid");
            }

            if (IsAllLettersOrDigits(user.email!.Replace("@", "").Replace(".", "")) == false)
            {
                return (false, $"Email can contain only latin letters or digits");
            }

            if (user.email.Length > MaxEmailLength)
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
