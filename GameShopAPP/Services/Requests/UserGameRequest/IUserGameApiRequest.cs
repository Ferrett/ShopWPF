using GameShopAPP.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public interface IUserGameApiRequest
    {
        Task<HttpResponseMessage> PostUserGameRequest(UserGame userGame);
        Task<HttpResponseMessage> GetAllUserGamesRequest();
        Task<HttpResponseMessage> GetGamesByUserIDRequest(int userID);
        Task<HttpResponseMessage> GetUsersByGameIDRequest(int gameID);
        Task<HttpResponseMessage> DeleteUserGameRequest(int userGameID);
    }
}
