using System;
using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Requests;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using GameShopAPP.Services.Validation;
using System.Text;
using System.Windows.Navigation;
using System.Windows.Input;
using GameShopAPP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using GameShopAPP.Services.Navigation;
using System.IO;

namespace GameShopAPP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public RelayCommand LogInCommand { get; }
        public NavigateCommand<RegistrationViewModel> NavigateRegistrationCommand { get; }

        private readonly IAuthenticationApiRequest _authenticationApiRequest;
        private readonly ILoginModelValidation _loginModelValidation;

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private LoginModel _loginModel;
        public LoginModel LoginModel
        {
            get { return _loginModel; }
            set
            {
                _loginModel = value;
                OnPropertyChanged("LoginModel");
            }
        }

        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        private string _responseText;
        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                _responseText = value;
                OnPropertyChanged("ResponseText");
            }
        }


        public LoginViewModel(IAuthenticationApiRequest authenticationApiRequest, ILoginModelValidation loginModelValidation, NavigationStore navigationStore)
        {
            NavigateRegistrationCommand = new NavigateCommand<RegistrationViewModel>(navigationStore, () => new RegistrationViewModel(
                DIContainer.ServiceProvider!.GetRequiredService<IAuthenticationApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IRegistrationModelValidation>(),
                navigationStore));

            _authenticationApiRequest = authenticationApiRequest;
            _loginModelValidation = loginModelValidation;

            LoginModel = new LoginModel();

            IsLoading = false;
            ResponseText = string.Empty;

            LogInCommand = new RelayCommand(LogIn);
            
        }

      

        public async void LogIn()
        {
            IsLoading = true;
            ResponseText = string.Empty;

            await CheckUser();

            IsLoading = false;
        }

        private void SaveToken(string token)
        {
            ApiConfig.UpdateToken(token);
        }

        private async Task CheckUser()
        {
            try
            {
                var validationResult = _loginModelValidation.Validate(LoginModel);
                if (validationResult.result == false)
                {
                    ResponseText = validationResult.errorMessage;
                    return;
                }

                var response = await _authenticationApiRequest.UserLogin(LoginModel);

                if (response.IsSuccessStatusCode)
                {
                    ResponseText = "Successfuly";
                    var responseData = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseData);
                    string token = deserializedResponse!.Values.First();

                    SaveToken(token);
                    ShopWindow shopWindow = new ShopWindow();
                    Application.Current.MainWindow.Close();
                    shopWindow.Show();
                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseData);
                    List<string> errorList = deserializedResponse!.Values.First().ToList();
                    errorList.ForEach(x => ResponseText += x + "\r\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
