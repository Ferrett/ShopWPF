using System;
using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using GameShopAPP.Services.Validation;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using GameShopAPP.Services.Navigation;
using System.Net.Http;

namespace GameShopAPP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public NavigateCommand<RegistrationViewModel> NavigateRegistrationCommand { get; }
        public RelayCommand LogInCommand { get; }

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

            LogInCommand = new RelayCommand(LogInUser);
        }

        public async void LogInUser(object? parameter)
        {
            IsLoading = true;
            ResponseText = string.Empty;

            await TryLogIn();

            IsLoading = false;
        }

        private async void OpenShopWindow(string login)
        {
            var responseMessage = await _userApiRequest.GetUserByLoginRequest(login);

            User user = JsonSerializer.Deserialize<User>(await responseMessage.Content.ReadAsStringAsync())!;

            ShopWindow shopWindow = new ShopWindow(user);
            Application.Current.MainWindow.Close();
            shopWindow.Show();
        }

        private async Task TryLogIn()
        {
            var validationResult = _loginModelValidation.Validate(LoginModel);
            if (validationResult.result == false)
            {
                ResponseText = validationResult.errorMessage;
                return;
            }

            var response = await _authenticationApiRequest.UserLoginRequest(LoginModel);

            if (response.IsSuccessStatusCode)
            {
                LogInSuccess(await response.Content.ReadAsStringAsync());
            }
            else
            {
                LogInFail(await response.Content.ReadAsStringAsync());
            }
        }

        private void LogInSuccess(string responseData)
        {
            ResponseText = "Log in successfull";

            var deserializedResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(responseData);
            string token = deserializedResponse!.Values.First();
            ApiConfig.UpdateToken(token);

            OpenShopWindow(LoginModel.login);
        }

        private void LogInFail(string responseData)
        {
            var deserializedResponse = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseData);
            List<string> errorList = deserializedResponse!.Values.First().ToList();
            errorList.ForEach(x => ResponseText += x + "\r\n");
        }
    }
}
