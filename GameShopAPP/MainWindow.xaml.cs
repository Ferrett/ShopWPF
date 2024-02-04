using GameShopAPP.Services.Requests;
using GameShopAPP.Services;
using GameShopAPP.ViewModels;
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

using GameShopAPP.Services.Validation;
using GameShopAPP.Services.Navigation;

namespace GameShopAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowViewModel windowViewModel;
        public MainWindow()
        {
            InitializeComponent();

            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new LoginViewModel(
             DIContainer.ServiceProvider.GetRequiredService<IAuthenticationApiRequest>(),
             DIContainer.ServiceProvider.GetRequiredService<IUserApiRequest>(),
             DIContainer.ServiceProvider.GetRequiredService<ILoginModelValidation>(),
             DIContainer.ServiceProvider.GetRequiredService<IUserValidation>(),
            navigationStore);
            //navigationStore.CurrentViewModel = new HomeViewModel(
            // navigationStore);

            windowViewModel = new WindowViewModel(navigationStore);
            this.DataContext = windowViewModel;
        }

    }
}
