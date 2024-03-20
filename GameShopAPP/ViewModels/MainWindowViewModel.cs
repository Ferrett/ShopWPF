using GameShopAPP.Models;
using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Windows;

namespace GameShopAPP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        private readonly IUserApiRequest _userApiRequest;

        public MainWindowViewModel(IUserApiRequest userApiRequest, NavigationStore navigationStore)
        {
            _userApiRequest = userApiRequest;
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            TryLogIn();
        }

        public async void TryLogIn()
        {
            if (ApiConfig.Token == null)
                return;

            Application.Current.MainWindow.Visibility = Visibility.Hidden;

            var securityToken = new JwtSecurityTokenHandler().ReadToken(ApiConfig.Token) as JwtSecurityToken;
            var loginClaim = securityToken!.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            var responseMessage = await _userApiRequest.GetUserByLoginRequest(loginClaim!.Value);

            if (responseMessage.IsSuccessStatusCode && responseMessage.StatusCode!=System.Net.HttpStatusCode.NoContent)
            {
                User user = JsonSerializer.Deserialize<User>(await responseMessage.Content.ReadAsStringAsync())!;
                ShopWindow shopWindow = new ShopWindow(user);

                Application.Current.MainWindow.Close();
                shopWindow.Show();
            }
            else
                Application.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
