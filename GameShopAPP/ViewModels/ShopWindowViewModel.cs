using GameShopAPP.Models;
using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameShopAPP.ViewModels
{
    public class ShopWindowViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public NavigateCommand<SearchViewModel> NavigateSearchCommand { get; }
        public NavigateCommand<ProfileViewModel> NavigateProfileCommand { get; }
        public NavigateCommand<LibraryViewModel> NavigateLibraryCommand { get; }

        public RelayCommand SearchBarFocusCommand { get; private set; }

        private readonly string _defaultSearchText;

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
            _defaultSearchText = "Search...";
            SearchText = _defaultSearchText;
            AvatarUrl = new BitmapImage(new Uri(User.profilePictureURL));

            SearchBarFocusCommand = new RelayCommand(SearchBarGotFocus);
        }

        public void SearchBarGotFocus(object? parameter)
        {
            if (SearchText == _defaultSearchText)
                SearchText = string.Empty;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
