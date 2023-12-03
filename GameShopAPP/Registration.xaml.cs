using GameShopAPP.Services;
using GameShopAPP.Services.Requests.AuthenticationRequest;
using GameShopAPP.Services.Requests.UserRequest;
using GameShopAPP.Services.Validation.LoginValidation;
using GameShopAPP.Services.Validation.RegistrationValidation;
using GameShopAPP.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameShopAPP
{
    /// <summary>
    /// Interaction logic for RegistrationValidation.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private RegistrationViewModel registrationViewModel;
        public Registration()
        {
            InitializeComponent();
            registrationViewModel = new RegistrationViewModel(
                DIContainer.ServiceProvider.GetRequiredService<IAuthenticationApiRequest>(),
                DIContainer.ServiceProvider.GetRequiredService<IRegistrationModelValidation>());

            registrationViewModel!.RequestClose += (sender, args) => Close();

            this.DataContext = registrationViewModel;
        }
    }
}
