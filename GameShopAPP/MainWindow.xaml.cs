using GameShopAPP.Services.Requests.UserRequest;
using GameShopAPP.Services;
using GameShopAPP.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using GameShopAPP.Services.Validation.LoginValidation;
using GameShopAPP.Services.Requests.AuthenticationRequest;

namespace GameShopAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        public MainWindow()
        {
            InitializeComponent();

            mainViewModel = new MainViewModel(
                DIContainer.ServiceProvider.GetRequiredService<IAuthenticationApiRequest>(),
                DIContainer.ServiceProvider.GetRequiredService<ILoginModelValidation>());

            this.DataContext = mainViewModel;
        }
    }
}
