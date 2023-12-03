using GameShopAPP.Logic;
using GameShopAPP.Model;
using GameShopAPP.Model.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Requests.AuthenticationRequest;
using GameShopAPP.Services.Requests.UserRequest;
using GameShopAPP.Services.Validation.LoginValidation;
using GameShopAPP.Services.Validation.RegistrationValidation;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameShopAPP.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public RelayCommand BackCommand { get; }
        public RelayCommand RegisterCommand { get; }

        public event EventHandler RequestClose;
        private readonly IAuthenticationApiRequest authenticationApiRequest;
        private readonly IRegistrationModelValidation registrationModelValidation;

        private Model.ServiceModels.RegistrationModel _registrationModel;
        public Model.ServiceModels.RegistrationModel RegistrationModel
        {
            get { return _registrationModel; }
            set
            {
                _registrationModel = value;
                OnPropertyChanged("RegistrationModel");
            }
        }

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

        public RegistrationViewModel(IAuthenticationApiRequest _authenticationApiRequest, IRegistrationModelValidation _registrationModelValidation)
        {
            authenticationApiRequest = _authenticationApiRequest;
            registrationModelValidation = _registrationModelValidation;
            RegistrationModel = new Model.ServiceModels.RegistrationModel();

            IsLoading = false;
            ResponseText = string.Empty;

            BackCommand = new RelayCommand(CloseRegistration);
            RegisterCommand = new RelayCommand(Register);
        }

        public async Task RegisterNewUser()
        {
            try
            {
                var validationResult = registrationModelValidation.Validate(RegistrationModel);
                if (validationResult.result == false)
                {
                    ResponseText = validationResult.errorMessage;
                    return;
                }

                var response = await authenticationApiRequest.RegisterNewUser(RegistrationModel);

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



        private async void Register()
        {
            IsLoading = true;
            ResponseText = string.Empty;

            await RegisterNewUser();

            IsLoading = false;
        }

        private void CloseRegistration()
        {
            CloseWindow();
        }
        private void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void CloseWindow()
        {
            OnRequestClose();
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
