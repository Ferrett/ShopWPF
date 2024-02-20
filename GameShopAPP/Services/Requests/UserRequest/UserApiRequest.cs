using GameShopAPP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests
{
    public class UserApiRequest : IUserApiRequest
    {
        private readonly string BaseUrl;

        private readonly JsonSerializerSettings SerializerSettings;

        public UserApiRequest()
        {
            BaseUrl = ApiConfig.ApiURL;
            SerializerSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
        }

        public async Task<HttpResponseMessage> PostUserRequest(User user)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = $"{{" +
                        $"\"id\":\"0\"," +
                        $"\"login\":\"{user.login}\"," +
                        $"{(string.IsNullOrEmpty(user.password) ? "" : $"\"password\":\"{BCrypt.Net.BCrypt.HashPassword(user.password)}\",")}" +
                        $"\"nickname\":\"{user.nickname}\"," +
                        $"{(string.IsNullOrEmpty(user.email) ? "" : $"\"email\":\"{user.email}\",")}" +
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

        public async Task<HttpResponseMessage> GetUserRequest(int userID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"User/GetUser/{userID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<HttpResponseMessage> PutUserRequest(int userID, User user)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = $"{{" +
                        $"\"id\":\"0\"," +
                        $"\"login\":\"{user.login}\"," +
                        $"{(string.IsNullOrEmpty(user.password) ? "" : $"\"passwordHash\":\"{BCrypt.Net.BCrypt.HashPassword(user.password)}\",")}" +
                        $"\"nickname\":\"{user.nickname}\"," +
                        $"{(string.IsNullOrEmpty(user.email) ? "" : $"\"email\":\"{user.email}\",")}" +
                        $"\"creationDate\":{utcNowJson}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PutAsync(client.BaseAddress + $"User/PutUser/{userID}", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutUserLogoRequest(int userID, BitmapImage bitmapImage)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {

                    MultipartFormDataContent multipartContent = new MultipartFormDataContent();

                    if (bitmapImage != null)
                    {
                        byte[] imageBytes = ConvertBitmapImageToByteArray(bitmapImage);
                        multipartContent.Add(new ByteArrayContent(imageBytes), "logo", "logo");
                        return await client.PutAsync(client.BaseAddress + $"User/PutUserProfilePicture/{userID}", multipartContent);
                    }
                    else
                    {
                        return await client.PutAsync(client.BaseAddress + $"User/PutUserProfilePicture/{userID}", null);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private byte[] ConvertBitmapImageToByteArray(BitmapImage bitmapImage)
        {
            BitmapSource bitmapSource = bitmapImage;

            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);

                return stream.ToArray();
            }
        }

        public async Task<HttpResponseMessage> DeleteUserRequest(int userID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.DeleteAsync(client.BaseAddress + $"User/DeleteUser/{userID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
