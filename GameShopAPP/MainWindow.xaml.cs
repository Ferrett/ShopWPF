using GameShopAPP.Services.Requests;
using GameShopAPP.Services;
using GameShopAPP.ViewModels;
using System.Windows;
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
        private MainWindowViewModel mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();

            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new LoginViewModel(
             DIContainer.ServiceProvider!.GetRequiredService<IUserApiRequest>(),
             DIContainer.ServiceProvider!.GetRequiredService<IAuthenticationApiRequest>(),
             DIContainer.ServiceProvider!.GetRequiredService<ILoginModelValidation>(),
             navigationStore);

            mainWindowViewModel = new MainWindowViewModel(
                DIContainer.ServiceProvider!.GetRequiredService<IUserApiRequest>(), 
                navigationStore);

            this.DataContext = mainWindowViewModel;
        }
    }
}
