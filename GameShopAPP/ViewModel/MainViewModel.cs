
using GameShopAPP.Logic;
using GameShopAPP.Services;
using System.Collections.Specialized;
using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;


namespace GameShopAPP.ViewModel
{
    public class MainViewModel
    {
        public RelayCommand OpenRegistrationCommand { get; }
        public MainViewModel()
        {
            OpenRegistrationCommand = new RelayCommand(OpenRegistration);
        }

        private void OpenRegistration()
        {
            Registration registration = new Registration();
            registration.ShowDialog();
        }
    }


}
