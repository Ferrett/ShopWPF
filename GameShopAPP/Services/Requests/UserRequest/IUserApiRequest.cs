﻿using GameShopAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests
{
    public interface IUserApiRequest
    {
        Task<HttpResponseMessage> PostUserRequest(User user);
        Task<HttpResponseMessage> GetUserRequest(int userID);
        Task<HttpResponseMessage> PutUserRequest(int userID, User user);
        Task<HttpResponseMessage> PutUserLogoRequest(int userID, BitmapImage bitmapImage);
        Task<HttpResponseMessage> DeleteUserRequest(int userID);
    }
}
