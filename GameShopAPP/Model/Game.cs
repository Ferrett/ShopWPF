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
    public record Game : INotifyPropertyChanged
    {
        private int _id { get; set; }
        private string _name { get; set; } = null!;
        private string? _logoURL { get; set; }
        private float _price { get; set; }
        private DateTime _publishDate { get; set; }
        private int _achievementsCount { get; set; }
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
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("name");
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
        public float price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("price");
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
        public int achievementsCount
        {
            get { return _achievementsCount; }
            set
            {
                _achievementsCount = value;
                OnPropertyChanged("achievementsCount");
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
