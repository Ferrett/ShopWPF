using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services;
using GameShopAPP.ViewModels;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using GameShopAPP.Models;

namespace GameShopAPP
{
    /// <summary>
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        private ShopWindowViewModel shopWindowViewModel;
        public ShopWindow(User user)
        {
            InitializeComponent();

            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new LibraryViewModel(
                DIContainer.ServiceProvider!.GetRequiredService<IGameStatsApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IUserGameApiRequest>(),
                user.id,
                navigationStore);

            shopWindowViewModel = new ShopWindowViewModel(user, navigationStore);

            this.DataContext = shopWindowViewModel;
        }
    }
}
