using GameShopAPP.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public interface IGameStatsApiRequest
    {
        Task<HttpResponseMessage> PostGameStatsRequest(GameStats gameStats);
        Task<HttpResponseMessage> GetAllGamesStatsRequest();
        Task<HttpResponseMessage> GetGameStatsRequest(int gameStatsID);
        Task<HttpResponseMessage> GetGameStatsByUserIDRequest(int userID);
        Task<HttpResponseMessage> PutGameStatsRequest(int gameStatsID, GameStats gameStats);
        Task<HttpResponseMessage> DeleteGameStatsRequest(int gameStatsID);
    }
}
