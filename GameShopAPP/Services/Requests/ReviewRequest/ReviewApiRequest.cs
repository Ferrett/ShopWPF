using GameShopAPP.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public class ReviewApiRequest : IReviewApiRequest
    {
        public async Task<HttpResponseMessage> PostReviewRequest(Review review)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    string postData = JsonSerializer.Serialize(review);
                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "Review/PostReview", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<HttpResponseMessage> GetAllReviewsRequest()
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Review/GetAllReviews");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetReviewRequest(int reviewID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Review/GetReview/{reviewID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetReviewsByGameIDRequest(int gameID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.GetAsync(client.BaseAddress + $"Review/GetReviewsByGameID/{gameID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutReviewRequest(int reviewID, Review review)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    string putData = JsonSerializer.Serialize(review);
                    StringContent content = new StringContent(putData, Encoding.UTF8, "application/json");
                    return await client.PutAsync(client.BaseAddress + $"Review/PutReview/{reviewID}", content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteReviewRequest(int reviewID)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(ApiConfig.ApiURL) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiConfig.Token}");
                    return await client.DeleteAsync(client.BaseAddress + $"Review/DeleteReview/{reviewID}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
