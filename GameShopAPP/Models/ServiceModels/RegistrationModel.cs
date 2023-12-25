using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Models.ServiceModels
{

    [Serializable]
    public class RegistrationModel : INotifyPropertyChanged
    {
        private string _login { get; set; } = null!;
        private string _password { get; set; } = null!;
        private string _nickname { get; set; } = null!;
        private string? _email { get; set; }

        public string login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("login");
            }
        }
        public string password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("password");
            }
        }
        public string nickname
        {
            get { return _nickname; }
            set
            {
                _nickname = value;
                OnPropertyChanged("nickname");
            }
        }
        public string? email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("email");
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
