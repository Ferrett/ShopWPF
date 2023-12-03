using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Model
{
    [Serializable]
    public record GameStats : INotifyPropertyChanged
    {
        private int _id { get; set; }
        private float _hoursPlayed { get; set; }
        private int _achievementsGotten { get; set; }
        private DateTime _purchaseDate { get; set; }
        private int _userID { get; set; }
        private int _gameID { get; set; }

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("id");
            }
        }
        public float hoursPlayed
        {
            get { return _hoursPlayed; }
            set
            {
                _hoursPlayed = value;
                OnPropertyChanged("hoursPlayed");
            }
        }
        public int achievementsGotten
        {
            get { return _achievementsGotten; }
            set
            {
                _achievementsGotten = value;
                OnPropertyChanged("achievementsGotten");
            }
        }
        public DateTime purchaseDate
        {
            get { return _purchaseDate; }
            set
            {
                _purchaseDate = value;
                OnPropertyChanged("purchaseDate");
            }
        }
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
