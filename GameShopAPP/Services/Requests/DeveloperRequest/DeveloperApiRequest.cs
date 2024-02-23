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
using GameShopAPP.Models.ServiceModels;

namespace GameShopAPP.Services.Requests
{
    public class DeveloperApiRequest : IDeveloperApiRequest
    {
        public async Task<HttpResponseMessage> PostDeveloperRequest(Developer developer)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    string postData = JsonSerializer.Serialize(developer);
                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "Developer/PostDeveloper", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetDeveloperRequest(int developerID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Developer/GetDeveloper/{developerID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetAllDevelopersRequest()
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Developer/GetAllDevelopers");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutDeveloperRequest(int developerID, Developer developer)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    string putData = JsonSerializer.Serialize(developer); 
                    StringContent content = new StringContent(putData, Encoding.UTF8, "application/json");
                    return await client.PutAsync(client.BaseAddress + $"Developer/PutDeveloper/{developerID}", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutDeveloperLogoRequest(int developerID, BitmapImage bitmapImage)
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
                        return await client.PutAsync(client.BaseAddress + $"Developer/PutDeveloperLogo/{developerID}", multipartContent);
                    }
                    else
                    {
                        return await client.PutAsync(client.BaseAddress + $"Developer/PutDeveloperLogo/{developerID}", null);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteDeveloperRequest(int developerID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.DeleteAsync(client.BaseAddress + $"Developer/DeleteDeveloper/{developerID}");
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
