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
    public interface IGameApiRequest
    {
        Task<HttpResponseMessage> PostGameRequest(Game game);
        Task<HttpResponseMessage> GetAllGamesRequest();
        Task<HttpResponseMessage> GetGameRequest(int gameID);
        Task<HttpResponseMessage> GetGamesByTitle(string gameTitle);
        Task<HttpResponseMessage> PutGameRequest(int gameID, Game game);
        Task<HttpResponseMessage> PutGameLogoRequest(int gameID, BitmapImage bitmapImage);
        Task<HttpResponseMessage> DeleteGameRequest(int gameID);
    }
}
