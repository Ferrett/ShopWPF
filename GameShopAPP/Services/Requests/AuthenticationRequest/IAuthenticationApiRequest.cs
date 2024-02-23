﻿using GameShopAPP.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Requests
{
    public interface IAuthenticationApiRequest
    {
        public Task<HttpResponseMessage> RegisterNewUser(RegistrationModel registrationModel);
        public Task<HttpResponseMessage> UserLogin(LoginModel loginModel);
        public Task<HttpResponseMessage> VerifyToken();
    }
}
