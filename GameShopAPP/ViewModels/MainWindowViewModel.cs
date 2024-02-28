using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using GameShopAPP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace GameShopAPP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IUserApiRequest _userApiRequest;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public MainWindowViewModel(IUserApiRequest userApiRequest, NavigationStore navigationStore)
        {
            _userApiRequest = userApiRequest;
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            TryLogIn();
        }

        public async void TryLogIn()
        {
            if (ApiConfig.Token == null)
                return;

            Application.Current.MainWindow.Visibility = Visibility.Hidden;

            var securityToken = new JwtSecurityTokenHandler().ReadToken(ApiConfig.Token) as JwtSecurityToken;
            var loginClaim = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            var responseMessage = await _userApiRequest.GetUserByLogin(loginClaim.Value);

            if (responseMessage.IsSuccessStatusCode)
            {
                User user = JsonSerializer.Deserialize<User>(await responseMessage.Content.ReadAsStringAsync())!;
                ShopWindow shopWindow = new ShopWindow(user);

                Application.Current.MainWindow.Close();
                shopWindow.Show();

            }
            else
                Application.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
