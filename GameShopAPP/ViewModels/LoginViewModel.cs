using System;
using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Requests;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using GameShopAPP.Services.Validation;
using System.Text.Json;
using System.Windows.Navigation;
using System.Windows.Input;
using GameShopAPP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using GameShopAPP.Services.Navigation;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http;
using System.Threading;

namespace GameShopAPP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public RelayCommand LogInCommand { get; }
        public NavigateCommand<RegistrationViewModel> NavigateRegistrationCommand { get; }


        private readonly IAuthenticationApiRequest _authenticationApiRequest;
        private readonly IUserApiRequest _userApiRequest;
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


        public LoginViewModel(IUserApiRequest userApiRequest, IAuthenticationApiRequest authenticationApiRequest, ILoginModelValidation loginModelValidation, NavigationStore navigationStore)
        {
            NavigateRegistrationCommand = new NavigateCommand<RegistrationViewModel>(navigationStore, () => new RegistrationViewModel(
                DIContainer.ServiceProvider!.GetRequiredService<IUserApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IAuthenticationApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IRegistrationModelValidation>(),
                navigationStore));

            _userApiRequest = userApiRequest;
            _authenticationApiRequest = authenticationApiRequest;
            _loginModelValidation = loginModelValidation;

            LoginModel = new LoginModel();

            IsLoading = false;
            ResponseText = string.Empty;

            LogInCommand = new RelayCommand(LogIn);

        }



        public async void LogIn(object parameter)
        {
            IsLoading = true;
            ResponseText = string.Empty;

            await CheckUser();

            IsLoading = false;
        }

       

        private async void OpenShopWindow(string login)
        {
            var responseMessage = await _userApiRequest.GetUserByLogin(login);
              
            User user = JsonSerializer.Deserialize<User>(await responseMessage.Content.ReadAsStringAsync())!;

            ShopWindow shopWindow = new ShopWindow(user);
            Application.Current.MainWindow.Close();
            shopWindow.Show();
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
                    var deserializedResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(responseData);
                    string token = deserializedResponse!.Values.First();
                    ApiConfig.UpdateToken(token);

                    OpenShopWindow(LoginModel.login);
                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseData);
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
