﻿using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using GameShopAPP.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using GameShopAPP.Models;

namespace GameShopAPP.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public NavigateCommand<LibraryViewModel> NavigateHomeCommand { get; }
        public SearchViewModel(int userID, NavigationStore navigationStore)
        {
            //NavigateHomeCommand = new NavigateCommand<LibraryViewModel>(navigationStore, () => new LibraryViewModel(
            //    DIContainer.ServiceProvider!.GetRequiredService<IUserGameApiRequest>(),
            //    userID,
            //    navigationStore));

           
        }
    }
}
