using System;
using System.ComponentModel;

namespace GameShopAPP.Models
{
    [Serializable]
    public record Game : INotifyPropertyChanged
    {
        private int _id { get; set; }
        private string _title { get; set; } = null!;
        private string? _logoURL { get; set; }
        private float _priceUSD { get; set; }
        private DateTime _publishDate { get; set; }
        private int _achievementsAmount { get; set; }
        private int _developerID { get; set; }

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("id");
            }
        }

        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("title");
            }
        }

        public string? logoURL
        {
            get { return _logoURL; }
            set
            {
                _logoURL = value;
                OnPropertyChanged("logoURL");
            }
        }

        public float priceUSD
        {
            get { return _priceUSD; }
            set
            {
                _priceUSD = value;
                OnPropertyChanged("_priceUSD");
            }
        }

        public DateTime publishDate
        {
            get { return _publishDate; }
            set
            {
                _publishDate = value;
                OnPropertyChanged("publishDate");
            }
        }

        public int achievementsAmount
        {
            get { return _achievementsAmount; }
            set
            {
                _achievementsAmount = value;
                OnPropertyChanged("achievementsAmount");
            }
        }

        public int developerID
        {
            get { return _developerID; }
            set
            {
                _developerID = value;
                OnPropertyChanged("developerID");
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
