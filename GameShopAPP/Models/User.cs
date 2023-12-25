using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Models
{
    [Serializable]
    public record User : INotifyPropertyChanged
    {
        private int _id { get; set; }
        private string _login { get; set; } = null!;
        private string _password { get; set; } = null!;
        private string _nickname { get; set; } = null!;
        private string? _avatarURL { get; set; } 
        private string? _email { get; set; }
        private DateTime _creationDate { get; set; }

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("id");
            }
        }
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
        public string? avatarURL
        {
            get { return _avatarURL; }
            set
            {
                _avatarURL = value;
                OnPropertyChanged("avatarURL");
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
        public DateTime creationDate
        {
            get { return _creationDate; }
            set
            {
                _creationDate = value;
                OnPropertyChanged("creationDate");
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
