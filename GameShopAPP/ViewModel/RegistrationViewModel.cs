using GameShopAPP.Logic;
using GameShopAPP.Model;
using GameShopAPP.Services;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameShopAPP.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public RelayCommand CloseRegistrationCommand { get; }
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


        private readonly IUserApiRequest userApiRequest;
        public RegistrationViewModel(IUserApiRequest _userApiRequest)
        {
            userApiRequest = _userApiRequest;
            User = new User();
            CloseRegistrationCommand = new RelayCommand(CloseRegistration);
            RegisterCommand = new RelayCommand(Register);
        }

        public async Task PostRequest()
        {
            //var apiUrl = Configuration["ApiUrl"];
            await userApiRequest.PostRequest(user);
            MessageBox.Show("Test");
            //string responseData = await response.Content.ReadAsStringAsync();

            //if (response.IsSuccessStatusCode)
            //{
            //    MessageBox.Show(responseData);
            //}
            //else
            //{
            //    var deserializedResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);
            //    List<JArray> errorList = (deserializedResponse!["errors"] as JObject)?.Properties().Select(item => (JArray)item.Value).ToList() ?? new List<JArray>();
            //    List<string> errorStrings = errorList.Select(error => error.Last!.ToString()).ToList();

            //    errorStrings.ForEach(x => MessageBox.Show(x));
            //}


        }

        private async void Register()
        {
            await PostRequest();
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
