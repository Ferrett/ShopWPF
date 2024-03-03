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

namespace GameShopAPP.Services.Requests
{
    public class GameApiRequest : IGameApiRequest
    {
        public async Task<HttpResponseMessage> PostGameRequest(Game game)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    string postData = JsonSerializer.Serialize(game);
                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "Game/PostGame", content);
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Game/GetAllGames");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Game/GetGame/{gameID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetGamesByTitleRequest(string gameTitle)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Game/GetGamesByTitle/{gameTitle}");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    string putData = JsonSerializer.Serialize(game);
                    StringContent content = new StringContent(putData, Encoding.UTF8, "application/json");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
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

        public async Task<HttpResponseMessage> DeleteGameRequest(int gameID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.DeleteAsync(client.BaseAddress + $"Game/DeleteGame/{gameID}");
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
    }
}
