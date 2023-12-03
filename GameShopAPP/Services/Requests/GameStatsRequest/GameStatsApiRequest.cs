using GameShopAPP.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests.GameStatsRequest
{
    public class GameStatsApiRequest : IGameStatsApiRequest
    {
        private readonly string BaseUrl;

        private readonly JsonSerializerSettings SerializerSettings;

        public GameStatsApiRequest()
        {
            BaseUrl = ApiConfig.ApiURL;
            SerializerSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
        }

        public async Task<HttpResponseMessage> PostGameStatsRequest(GameStats gameStats)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = string.Empty;// $"{{" +
                    //    $"\"id\":\"0\"," +
                    //    $"\"login\":\"{gameStats.login}\"," +
                    //    $"{(String.IsNullOrEmpty(gameStats.password) ? "" : $"\"password\":\"{BCrypt.Net.BCrypt.HashPassword(gameStats.password)}\",")}" +
                    //    $"\"nickname\":\"{gameStats.nickname}\"," +
                    //    $"{(String.IsNullOrEmpty(gameStats.email) ? "" : $"\"email\":\"{gameStats.email}\",")}" +
                    //    $"\"creationDate\":{utcNowJson}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "GameStats/PostGameStats", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetGameStatsRequest(int gameStatsID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.GetAsync(client.BaseAddress + $"GameStats/GetGameStats/{gameStatsID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetAllGamesStatsRequest()
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.GetAsync(client.BaseAddress + $"GameStats/GetAllGamesStats");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutGameStatsRequest(int gameStatsID, GameStats gameStats)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = string.Empty;//$"{{" +
                                                   //$"\"id\":\"0\"," +
                                                   //$"\"login\":\"{gameStats.login}\"," +
                                                   //$"{(String.IsNullOrEmpty(gameStats.password) ? "" : $"\"passwordHash\":\"{BCrypt.Net.BCrypt.HashPassword(gameStats.password)}\",")}" +
                                                   //$"\"nickname\":\"{gameStats.nickname}\"," +
                                                   //$"{(String.IsNullOrEmpty(gameStats.email) ? "" : $"\"email\":\"{gameStats.email}\",")}" +
                                                   //$"\"creationDate\":{utcNowJson}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PutAsync(client.BaseAddress + $"GameStats/PutGameStats/{gameStatsID}", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteGameStatsRequest(int gameStatsID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.DeleteAsync(client.BaseAddress + $"GameStats/DeleteGameStats/{gameStatsID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
