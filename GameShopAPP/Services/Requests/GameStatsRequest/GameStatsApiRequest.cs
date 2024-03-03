using GameShopAPP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace GameShopAPP.Services.Requests
{
    public class GameStatsApiRequest : IGameStatsApiRequest
    {

        public async Task<HttpResponseMessage> PostGameStatsRequest(GameStats gameStats)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    string postData = JsonSerializer.Serialize(gameStats);
                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "GameStats/PostGameStats", content);
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"GameStats/GetAllGamesStats");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"GameStats/GetGameStats/{gameStatsID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetGameStatsByUserIDRequest(int userID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"GameStats/GetGameStatsByUserID/{userID}");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    string putData = JsonSerializer.Serialize(gameStats); 
                    StringContent content = new StringContent(putData, Encoding.UTF8, "application/json");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
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
