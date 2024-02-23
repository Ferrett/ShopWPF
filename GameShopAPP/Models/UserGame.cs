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
    public record UserGame : INotifyPropertyChanged
    {
        private int _userID{ get; set; }
        private int _gameID{ get; set; }

        public int userID
        {
            get { return _userID; }
            set
            {
                _userID = value;
                OnPropertyChanged("userID");
            }
        }

        public int gameID
        {
            get { return _gameID; }
            set
            {
                _gameID = value;
                OnPropertyChanged("gameID");
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
