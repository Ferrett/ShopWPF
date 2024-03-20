using GameShopAPP.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public interface IReviewApiRequest
    {
        Task<HttpResponseMessage> PostReviewRequest(Review review);
        Task<HttpResponseMessage> GetAllReviewsRequest();
        Task<HttpResponseMessage> GetReviewRequest(int reviewID);
        Task<HttpResponseMessage> GetReviewsByGameIDRequest(int gameID);
        Task<HttpResponseMessage> PutReviewRequest(int reviewID, Review review);
        Task<HttpResponseMessage> DeleteReviewRequest(int reviewID);
    }
}
