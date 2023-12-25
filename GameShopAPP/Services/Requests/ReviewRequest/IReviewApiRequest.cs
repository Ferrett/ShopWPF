using GameShopAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests
{
    public interface IReviewApiRequest
    {
        Task<HttpResponseMessage> PostReviewRequest(Review review);
        Task<HttpResponseMessage> GetReviewRequest(int reviewID);
        Task<HttpResponseMessage> GetAllReviewsRequest();
        Task<HttpResponseMessage> PutReviewRequest(int reviewID, Review review);
        Task<HttpResponseMessage> DeleteReviewRequest(int reviewID);
    }
}
