﻿using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public class AuthenticationApiRequest : IAuthenticationApiRequest
    {
        public async Task<HttpResponseMessage> RegisterNewUser(RegistrationModel registrationModel)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    string postData = JsonSerializer.Serialize(registrationModel);
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    string postData = JsonSerializer.Serialize(loginModel);
                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "Authentication/UserLogin", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> VerifyToken()
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Authentication/VerifyToken");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
