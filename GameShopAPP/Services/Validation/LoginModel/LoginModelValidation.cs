using GameShopAPP.Models.ServiceModels;

namespace GameShopAPP.Services.Validation
{
    public class LoginModelValidation : ILoginModelValidation
    {
        public (bool result, string errorMessage) Validate(LoginModel loginModel)
        {
            var loginValidationResult = ValidateLogin(loginModel);
            if (loginValidationResult.result == false)
                return (false, loginValidationResult.errorMessage);

            var passwordValidationResult = ValidatePassword(loginModel);
            if (passwordValidationResult.result == false)
                return (false, passwordValidationResult.errorMessage);

            return (true,string.Empty);
        }

        private const int MinLoginLength = 5;
        private const int MaxLoginLength = 99;
        private (bool result, string errorMessage) ValidateLogin(LoginModel loginModel)
        {
            if (loginModel.login.Length < MinLoginLength)
            {
                return (false, $"Login is too short");
            }

            if (loginModel.login.Length > MaxLoginLength)
            {
                return (false, $"Login is too long");
            }

            if (IsAllLettersOrDigits(loginModel.login) == false)
            {
                return (false, $"Login can contain only latin letters or digits");
            }

            return (true, string.Empty);
        }

        private const int MinPasswordLength = 7;
        private const int MaxPasswordLength = 256;
        private (bool result, string errorMessage) ValidatePassword(LoginModel loginModel)
        {
            if (loginModel.password.Length < MinPasswordLength)
            {
                return (false, $"Password is too short");
            }

            if (loginModel.password.Length > MaxPasswordLength)
            {
                return (false, $"Password is too long");
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
