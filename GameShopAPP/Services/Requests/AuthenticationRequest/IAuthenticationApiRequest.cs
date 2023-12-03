using GameShopAPP.Model.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests.AuthenticationRequest
{
    public interface IAuthenticationApiRequest
    {
        public Task<HttpResponseMessage> RegisterNewUser(RegistrationModel registrationModel);
        public Task<HttpResponseMessage> UserLogin(LoginModel loginModel);
    }
}
