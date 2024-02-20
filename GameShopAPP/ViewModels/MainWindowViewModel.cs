using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using GameShopAPP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
            Application.Current.MainWindow.Visibility = Visibility.Hidden;
            var response = await _userApiRequest.GetUserRequest(0);

            if (response.IsSuccessStatusCode)
            {
                ShopWindow shopWindow = new ShopWindow();
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
