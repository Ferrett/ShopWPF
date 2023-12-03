using System;
using GameShopAPP.Logic;
using GameShopAPP.Model;
using GameShopAPP.Model.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Requests.UserRequest;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using GameShopAPP.Services.Validation.LoginValidation;
using GameShopAPP.Services.Requests.AuthenticationRequest;

namespace GameShopAPP.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand OpenRegistrationCommand { get; }
        public RelayCommand LogInCommand { get; }

        private readonly IAuthenticationApiRequest authenticationApiRequest;
        private readonly ILoginModelValidation loginModelValidation;

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

        public MainViewModel(IAuthenticationApiRequest _authenticationApiRequest, ILoginModelValidation _loginModelValidation)
        {
            authenticationApiRequest = _authenticationApiRequest;
            loginModelValidation = _loginModelValidation;

            LoginModel = new LoginModel();

            IsLoading = false;
            ResponseText = string.Empty;

            OpenRegistrationCommand = new RelayCommand(OpenRegistration);
            LogInCommand = new RelayCommand(LogIn);
        }

        private void OpenRegistration()
        {
            Registration registration = new Registration();
            registration.ShowDialog();
        }

        public async void LogIn()
        {
            IsLoading = true;
            ResponseText = string.Empty;

            await CheckUser();

            IsLoading = false;
        }

        private async Task CheckUser()
        {
            try
            {
                var validationResult = loginModelValidation.Validate(LoginModel);
                if (validationResult.result == false)
                {
                    ResponseText = validationResult.errorMessage;
                    return;
                }

                var response = await authenticationApiRequest.UserLogin(LoginModel);

                if (response.IsSuccessStatusCode)
                {
                    ResponseText = "Successfuly";
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


}
