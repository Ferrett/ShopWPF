using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace GameShopAPP.ViewModels
{
    public class ShopWindowViewModel : ViewModelBase
    {

        public NavigateCommand<SearchViewModel> NavigateSearchCommand { get; }
        public NavigateCommand<ProfileViewModel> NavigateProfileCommand { get; }
        public NavigateCommand<LibraryViewModel> NavigateLibraryCommand { get; }

        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public RelayCommand SearchBarFocusCommand { get; private set; }

        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        private ImageSource _avatarUrl;
        public ImageSource AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                _avatarUrl = value;
                OnPropertyChanged("AvatarUrl");
            }
        }
        public ShopWindowViewModel(User user, NavigationStore navigationStore)
        {
            NavigateSearchCommand = new NavigateCommand<SearchViewModel>(navigationStore, () => new SearchViewModel(
                DIContainer.ServiceProvider!.GetRequiredService<IDeveloperApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IUserGameApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IGameApiRequest>(),
                SearchText, user.id, navigationStore));

            NavigateProfileCommand = new NavigateCommand<ProfileViewModel>(navigationStore, () => new ProfileViewModel(
                DIContainer.ServiceProvider!.GetRequiredService<IUserApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IRegistrationModelValidation>(),
                user.id));

            NavigateLibraryCommand = new NavigateCommand<LibraryViewModel>(navigationStore, () => new LibraryViewModel(
                DIContainer.ServiceProvider!.GetRequiredService<IGameStatsApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IUserGameApiRequest>(),
                user.id, navigationStore));

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            User = user;
            SearchText = "Search...";
            AvatarUrl = new BitmapImage(new Uri(User.profilePictureURL));

            SearchBarFocusCommand = new RelayCommand(SearchBarGotFocus);
        }

        public void SearchBarGotFocus(object parameter)
        {
            if (SearchText == "Search...")
                SearchText = string.Empty;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
