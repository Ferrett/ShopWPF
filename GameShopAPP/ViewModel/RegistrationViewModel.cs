using GameShopAPP.Logic;
using GameShopAPP.Model;
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
    public class RegistrationViewModel :  INotifyPropertyChanged
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

        public RegistrationViewModel()
        {
            User = new User();
            CloseRegistrationCommand = new RelayCommand(CloseRegistration);
            RegisterCommand = new RelayCommand(Register);
        }

        public async Task PostRequest()
        {
            //https://localhost:7087/User/PostUser
            // HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync($"{Routes.APIurl}/{tableName}/{Routes.GetRequest}");

            //return response;


            string apiUrl = @"https://kqntok5tzzjfpcuhpo5xvpwqcu0absdx.lambda-url.eu-north-1.on.aws/User/PostUser";

            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
            var json = JsonConvert.SerializeObject(DateTime.UtcNow, settings);

            string postData = $"{{" +
                $"\"id\":\"0\"," +
                $"\"login\":\"{User.login}\"," +
                $"\"passwordHash\":\"{User.passwordHash}\"," +
                $"\"nickname\":\"{User.nickname}\"," +
                $"\"profilePictureURL\":\"\"," +
                $"\"email\":\"{User.email}\"," +
                $"\"creationDate\":{json}}}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    StringContent content = new StringContent(postData, Encoding.UTF8, "application/json");

                    response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(responseData);
                       
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception: {ex.Message}");
                }
               

                
            }
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
