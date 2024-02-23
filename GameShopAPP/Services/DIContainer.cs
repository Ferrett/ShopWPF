﻿using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using GameShopAPP.Views;
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
        public static IServiceProvider? ServiceProvider { get; private set; }

        public static void Initialize()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<ShopWindow>();
            services.AddSingleton<LoginView>();
            services.AddSingleton<RegistrationView>();
            services.AddSingleton<HomeView>();
            services.AddSingleton<SearchView>();
            services.AddSingleton<GameView>();
            services.AddSingleton<ProfileView>();

            services.AddSingleton<IAuthenticationApiRequest, AuthenticationApiRequest>();
            services.AddSingleton<IDeveloperApiRequest, DeveloperApiRequest>();
            services.AddSingleton<IGameApiRequest, GameApiRequest>();
            services.AddSingleton<IGameStatsApiRequest, GameStatsApiRequest>();
            services.AddSingleton<IReviewApiRequest, ReviewApiRequest>();
            services.AddSingleton<IUserApiRequest, UserApiRequest>();
            services.AddSingleton<IUserGameApiRequest, UserGameApiRequest>();

            services.AddSingleton<IDeveloperValidation, DeveloperValidation>();
            services.AddSingleton<IGameValidation, GameValidation>();
            services.AddSingleton<IGameStatsValidation, GameStatsValidation>();
            services.AddSingleton<ILoginModelValidation, LoginModelValidation>();
            services.AddSingleton<IRegistrationModelValidation, RegistrationModelValidation>();
            services.AddSingleton<IReviewValidation, ReviewValidation>();   
            services.AddSingleton<IUserValidation, UserValidation>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
