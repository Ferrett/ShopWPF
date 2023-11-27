using GameShopAPP.Logic;
using GameShopAPP.Model;
using GameShopAPP.Services;
using GameShopAPP.Services.UserRequest;
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

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
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

        private readonly IUserApiRequest userApiRequest;
        public RegistrationViewModel(IUserApiRequest _userApiRequest)
        {
            userApiRequest = _userApiRequest;
            User = new User();

            IsLoading = false;
            ResponseText = string.Empty;

            BackCommand = new RelayCommand(CloseRegistration);
            RegisterCommand = new RelayCommand(Register);
        }

        public async Task RegisterNewUser()
        {
            try
            {
                ResponseText = string.Empty;
                IsLoading = true;
                var response = await userApiRequest.PostUserRequest(user);
                //var response = await userApiRequest.GetUserRequest(99);
                //var response = await userApiRequest.PutUserRequest(1,user);
                //var response = await userApiRequest.DeleteUserRequest(5);

                var response2 = await userApiRequest.PutUserLogoRequest(4, new BitmapImage(new Uri(@"C:\Users\User\Desktop\pira.jpg")));
                IsLoading = false;

                if (response.IsSuccessStatusCode)
                {
                    ResponseText = "Successfuly";
                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);
                    List<JArray> errorList = (deserializedResponse!["errors"] as JObject)?.Properties().Select(item => (JArray)item.Value).ToList() ?? new List<JArray>();
                    List<string> errorStrings = errorList.Select(error => error.Last!.ToString()).ToList();

                    errorStrings.ForEach(x => ResponseText += x + "\r\n");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private async void Register()
        {
            await RegisterNewUser();
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
