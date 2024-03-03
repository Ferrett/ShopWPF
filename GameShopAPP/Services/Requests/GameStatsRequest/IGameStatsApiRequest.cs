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
