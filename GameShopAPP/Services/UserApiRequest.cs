using GameShopAPP.Model;
using GameShopAPP.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GameShopAPP.Logic
{
    public class UserApiRequest : IUserApiRequest
    {
        private readonly string BaseUrl;
        
        private readonly JsonSerializerSettings SerializerSettings;

        public UserApiRequest()
        {
            BaseUrl = ApiConfig.ApiURL;// @"https://kqntok5tzzjfpcuhpo5xvpwqcu0absdx.lambda-url.eu-north-1.on.aws";
            SerializerSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
        }

        public async Task<HttpResponseMessage> PostRequest(User user)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl)})
                {
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = $"{{" +
                        $"\"id\":\"0\"," +
                        $"\"login\":\"{user.login}\"," +
                        $"{(String.IsNullOrEmpty(user.passwordHash) ? "" : $"\"passwordHash\":\"{BCrypt.Net.BCrypt.HashPassword(user.passwordHash)}\",")}" +
                        $"\"nickname\":\"{user.nickname}\"," +
                        $"{(String.IsNullOrEmpty(user.email) ? "" : $"\"email\":\"{user.email}\",")}" +
                        $"\"creationDate\":{utcNowJson}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "User/PostUser", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<HttpResponseMessage> GetRequest(int userID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            return response;
        }

        public async Task<HttpResponseMessage> PutRequest(int userID, User user)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            return response;
        }


        public async Task<HttpResponseMessage> DeleteRequest(int userID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            return response;
        }
    }
}
