using GameShopAPP.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameShopAPP.ViewModel
{
    public class RegistrationViewModel
    {
        public RelayCommand CloseRegistrationCommand { get; }
        public event EventHandler RequestClose;
        public RegistrationViewModel()
        {
            CloseRegistrationCommand = new RelayCommand(CloseRegistration);
        }

        private void CloseRegistration()
        {
            CloseWindow();
        }
        private void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        // ЗАКИНУТЬ НА ГИТ
        private void CloseWindow()
        {
            OnRequestClose();
        }
    }
}
