using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using GameShopAPP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GameShopAPP.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {

        public RelayCommand RegisterCommand { get; }

        public ICommand NavigateLoginCommand { get; }
        private readonly IAuthenticationApiRequest _authenticationApiRequest;
        private readonly IRegistrationModelValidation _registrationModelValidation;

        private RegistrationModel _registrationModel;
        public RegistrationModel RegistrationModel
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

        public RegistrationViewModel(IAuthenticationApiRequest authenticationApiRequest, IRegistrationModelValidation registrationModelValidation, NavigationStore navigationStore)
        {
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(
            DIContainer.ServiceProvider.GetRequiredService<IAuthenticationApiRequest>(),
            DIContainer.ServiceProvider.GetRequiredService<IUserApiRequest>(),
            DIContainer.ServiceProvider.GetRequiredService<ILoginModelValidation>(),
            DIContainer.ServiceProvider.GetRequiredService<IUserValidation>(),
            navigationStore));

            _authenticationApiRequest = authenticationApiRequest;
            _registrationModelValidation = registrationModelValidation;
            RegistrationModel = new RegistrationModel();

            IsLoading = false;
            ResponseText = string.Empty;


            RegisterCommand = new RelayCommand(Register);
        }

        public async Task RegisterNewUser()
        {
            try
            {
                var validationResult = _registrationModelValidation.Validate(RegistrationModel);
                if (validationResult.result == false)
                {
                    ResponseText = validationResult.errorMessage;
                    return;
                }

                var response = await _authenticationApiRequest.RegisterNewUser(RegistrationModel);

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
    }
}
