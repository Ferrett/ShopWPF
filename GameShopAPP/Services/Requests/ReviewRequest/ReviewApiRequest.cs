using GameShopAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public class ReviewApiRequest : IReviewApiRequest
    {
        private readonly string BaseUrl;

        private readonly JsonSerializerSettings SerializerSettings;

        public ReviewApiRequest()
        {
            BaseUrl = ApiConfig.ApiURL;
            SerializerSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
        }

        public async Task<HttpResponseMessage> PostReviewRequest(Review review)
        {
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = string.Empty;// $"{{" +
                    //    $"\"id\":\"0\"," +
                    //    $"\"login\":\"{review.login}\"," +
                    //    $"{(String.IsNullOrEmpty(review.password) ? "" : $"\"password\":\"{BCrypt.Net.BCrypt.HashPassword(review.password)}\",")}" +
                    //    $"\"nickname\":\"{review.nickname}\"," +
                    //    $"{(String.IsNullOrEmpty(review.email) ? "" : $"\"email\":\"{review.email}\",")}" +
                    //    $"\"creationDate\":{utcNowJson}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                    return await client.PostAsync(client.BaseAddress + "Review/PostReview", content);
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.GetAsync(client.BaseAddress + $"Review/GetReview/{reviewID}");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    return await client.GetAsync(client.BaseAddress + $"Review/GetAllReviews");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
                    string utcNowJson = JsonConvert.SerializeObject(DateTime.UtcNow, SerializerSettings);

                    string postData = string.Empty;//$"{{" +
                                                   //$"\"id\":\"0\"," +
                                                   //$"\"login\":\"{review.login}\"," +
                                                   //$"{(String.IsNullOrEmpty(review.password) ? "" : $"\"passwordHash\":\"{BCrypt.Net.BCrypt.HashPassword(review.password)}\",")}" +
                                                   //$"\"nickname\":\"{review.nickname}\"," +
                                                   //$"{(String.IsNullOrEmpty(review.email) ? "" : $"\"email\":\"{review.email}\",")}" +
                                                   //$"\"creationDate\":{utcNowJson}}}";

                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");
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
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30), BaseAddress = new Uri(BaseUrl) })
                {
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
