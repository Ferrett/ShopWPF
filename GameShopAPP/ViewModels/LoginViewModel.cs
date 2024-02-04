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

namespace GameShopAPP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public RelayCommand LogInCommand { get; }
        public RelayCommand TestCommand { get; }
        public ICommand NavigateRegistrationCommand { get; }

        private readonly IAuthenticationApiRequest _authenticationApiRequest;
        private readonly ILoginModelValidation _loginModelValidation;
        private readonly IUserValidation _userValidation;
        private readonly IUserApiRequest _userApiRequest;

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
        
       
        public LoginViewModel(IAuthenticationApiRequest authenticationApiRequest, IUserApiRequest userApiRequest, ILoginModelValidation loginModelValidation, IUserValidation userValidation, NavigationStore navigationStore)
        {
            NavigateRegistrationCommand = new NavigateCommand<RegistrationViewModel>(navigationStore,() => new RegistrationViewModel(
                DIContainer.ServiceProvider.GetRequiredService<IAuthenticationApiRequest>(),
                DIContainer.ServiceProvider.GetRequiredService<IRegistrationModelValidation>(),
                navigationStore));

            _authenticationApiRequest = authenticationApiRequest;
            _loginModelValidation = loginModelValidation;
            _userValidation = userValidation;
            _userApiRequest = userApiRequest;
            
            LoginModel = new LoginModel();

            IsLoading = false;
            ResponseText = string.Empty;

            LogInCommand = new RelayCommand(LogIn);
            TestCommand = new RelayCommand(Test2);

            token = string.Empty;
        }

       
        private async void Test2()
        {
            await Test();
        }
        private string token;
        private async Task Test()
        {
            try
            {
                User user = new User()
                {
                    password = "password",
                    nickname = "username",
                    login = "ddddddddddd"
                };

                var validationResult = _userValidation.Validate(user);
                if (validationResult.result == false)
                {
                    ResponseText = validationResult.errorMessage;
                    return;
                }


                var response = await _userApiRequest.GetAllUsersRequest(token);

                if (response.IsSuccessStatusCode)
                {
                    ResponseText = "SuccessfulyT";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ResponseText = "Unauthorized";
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

        public async void LogIn()
        {
            IsLoading = true;
            ResponseText = string.Empty;

            await CheckUser();

            IsLoading = false;
        }

        public class TokenData
        {
            public string Token { get; set; }
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
                    var deserializedResponse = JsonConvert.DeserializeObject<TokenData>(responseData);
                    token = deserializedResponse!.Token;

                    ShopWindow a = new ShopWindow();
                    Application.Current.MainWindow.Close();
                    a.Show();
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
