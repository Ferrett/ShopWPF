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

namespace GameShopAPP.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public RelayCommand TestCommand { get; }
        public ICommand NavigateHomeCommand { get; }
        public SearchViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(
                navigationStore));

           
        }
    }
}
