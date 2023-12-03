using GameShopAPP.Logic;
using GameShopAPP.Services.Requests.AuthenticationRequest;
using GameShopAPP.Services.Requests.UserRequest;
using GameShopAPP.Services.Validation.LoginValidation;
using GameShopAPP.Services.Validation.RegistrationValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services
{
    public static class DIContainer
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Initialize()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IUserApiRequest, UserApiRequest>();
            services.AddSingleton<IAuthenticationApiRequest, AuthenticationApiRequest>();
            services.AddSingleton<ILoginModelValidation, LoginModelValidation>();
            services.AddSingleton<IRegistrationModelValidation, RegistrationModelValidation>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
