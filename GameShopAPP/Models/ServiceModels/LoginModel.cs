﻿using System;
using System.ComponentModel;

namespace GameShopAPP.Models.ServiceModels
{
    [Serializable]
    public class LoginModel : INotifyPropertyChanged
    {
        private string _login { get; set; } = null!;
        private string _password { get; set; } = null!;

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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
