using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public class AuthenticationApiRequest : IAuthenticationApiRequest
    {
        private readonly string BaseUrl;

        public AuthenticationApiRequest()
        {
            BaseUrl = ApiConfig.ApiURL;
        }

        public async Task<HttpResponseMessage> RegisterNewUser(RegistrationModel registrationModel)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string postData = $"{{" +
                        $"\"login\":\"{registrationModel.login}\"," +
                        $"\"password\":\"{registrationModel.password}\"," +
                        $"\"nickname\":\"{registrationModel.login}\"" +
                        $"{(string.IsNullOrEmpty(registrationModel.email) ? "" : $",\"email\":\"{registrationModel.email}\"")}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "Authentication/RegisterNewUser", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> UserLogin(LoginModel loginModel)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string postData = $"{{" +
                        $"\"login\":\"{loginModel.login}\"," +
                        $"\"password\":\"{loginModel.password}\"}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "Authentication/UserLogin", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
