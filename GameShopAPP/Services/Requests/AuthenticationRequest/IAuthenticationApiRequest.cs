using GameShopAPP.Models.ServiceModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public interface IAuthenticationApiRequest
    {
        public Task<HttpResponseMessage> RegisterNewUserRequest(RegistrationModel registrationModel);
        public Task<HttpResponseMessage> UserLoginRequest(LoginModel loginModel);
    }
}
