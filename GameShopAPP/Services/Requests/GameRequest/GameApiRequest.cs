using GameShopAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests
{
    public class GameApiRequest : IGameApiRequest
    {
        private readonly string BaseUrl;

        private readonly JsonSerializerSettings SerializerSettings;

        public GameApiRequest()
        {
            BaseUrl = ApiConfig.ApiURL;
            SerializerSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
        }

        public async Task<HttpResponseMessage> PostGameRequest(Game game)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = string.Empty;// $"{{" +
                    //    $"\"id\":\"0\"," +
                    //    $"\"login\":\"{game.login}\"," +
                    //    $"{(String.IsNullOrEmpty(game.password) ? "" : $"\"password\":\"{BCrypt.Net.BCrypt.HashPassword(game.password)}\",")}" +
                    //    $"\"nickname\":\"{game.nickname}\"," +
                    //    $"{(String.IsNullOrEmpty(game.email) ? "" : $"\"email\":\"{game.email}\",")}" +
                    //    $"\"creationDate\":{utcNowJson}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "Game/PostGame", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetGameRequest(int gameID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.GetAsync(client.BaseAddress + $"Game/GetGame/{gameID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetAllGamesRequest()
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.GetAsync(client.BaseAddress + $"Game/GetAllGames");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutGameRequest(int gameID, Game game)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = string.Empty;//$"{{" +
                                                   //$"\"id\":\"0\"," +
                                                   //$"\"login\":\"{game.login}\"," +
                                                   //$"{(String.IsNullOrEmpty(game.password) ? "" : $"\"passwordHash\":\"{BCrypt.Net.BCrypt.HashPassword(game.password)}\",")}" +
                                                   //$"\"nickname\":\"{game.nickname}\"," +
                                                   //$"{(String.IsNullOrEmpty(game.email) ? "" : $"\"email\":\"{game.email}\",")}" +
                                                   //$"\"creationDate\":{utcNowJson}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PutAsync(client.BaseAddress + $"Game/PutGame/{gameID}", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutGameLogoRequest(int gameID, BitmapImage bitmapImage)
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
                        return await client.PutAsync(client.BaseAddress + $"Game/PutGameLogo/{gameID}", multipartContent);
                    }
                    else
                    {
                        return await client.PutAsync(client.BaseAddress + $"Game/PutGameLogo/{gameID}", null);
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

        public async Task<HttpResponseMessage> DeleteGameRequest(int gameID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.DeleteAsync(client.BaseAddress + $"Game/DeleteGame/{gameID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
