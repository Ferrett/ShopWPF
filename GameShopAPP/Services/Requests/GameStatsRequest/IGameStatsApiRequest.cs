using GameShopAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests.GameStatsRequest
{
    public interface IGameStatsApiRequest
    {
        Task<HttpResponseMessage> PostGameStatsRequest(GameStats gameStats);
        Task<HttpResponseMessage> GetGameStatsRequest(int gameStatsID);
        Task<HttpResponseMessage> GetAllGamesStatsRequest();
        Task<HttpResponseMessage> PutGameStatsRequest(int gameStatsID, GameStats gameStats);
        Task<HttpResponseMessage> DeleteGameStatsRequest(int gameStatsID);
    }
}
