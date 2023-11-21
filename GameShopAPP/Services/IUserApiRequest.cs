using GameShopAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services
{
    public interface IUserApiRequest
    {
        Task<HttpResponseMessage> PostRequest(User user);
        Task<HttpResponseMessage> GetRequest(int userID);
        Task<HttpResponseMessage> PutRequest(int userID, User user);
        Task<HttpResponseMessage> DeleteRequest(int userID);
    }
}
