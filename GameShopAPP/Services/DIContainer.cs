using GameShopAPP.Logic;
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

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
