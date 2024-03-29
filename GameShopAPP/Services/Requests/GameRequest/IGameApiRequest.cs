﻿using GameShopAPP.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests
{
    public interface IGameApiRequest
    {
        Task<HttpResponseMessage> PostGameRequest(Game game);
        Task<HttpResponseMessage> GetAllGamesRequest();
        Task<HttpResponseMessage> GetGameRequest(int gameID);
        Task<HttpResponseMessage> GetGamesByTitleRequest(string gameTitle);
        Task<HttpResponseMessage> PutGameRequest(int gameID, Game game);
        Task<HttpResponseMessage> PutGameLogoRequest(int gameID, BitmapImage bitmapImage);
        Task<HttpResponseMessage> DeleteGameRequest(int gameID);
    }
}
