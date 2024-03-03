using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using GameShopAPP.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using GameShopAPP.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace GameShopAPP.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private ObservableCollection<Tuple<Game, Developer, bool>> _gameInfo;
        public ObservableCollection<Tuple<Game, Developer, bool>> GamesInfo
        {
            get { return _gameInfo; }
            set
            {
                _gameInfo = value;
                OnPropertyChanged("GameInfo");
            }
        }

        private readonly IUserGameApiRequest _userGameApiRequest;
        private readonly IDeveloperApiRequest _developerApiRequest;
        private readonly IGameApiRequest _gameApiRequest;

        public NavigateCommand<GameViewModel> NavigateGameCommand { get; }
        public RelayCommand GameTitleClickCommand { get; }

        private int _selectedGameID;
        public int SelectedGameID
        {
            get { return _selectedGameID; }
            set
            {
                _selectedGameID = value;
                OnPropertyChanged("SelectedGameID");
            }
        }

        public SearchViewModel() { }
        public SearchViewModel(IDeveloperApiRequest developerApiRequest, IUserGameApiRequest userGameApiRequest, IGameApiRequest gameApiRequest, string searchText, int userID, NavigationStore navigationStore)
        {
            NavigateGameCommand = new NavigateCommand<GameViewModel>(navigationStore, () => new GameViewModel(
                DIContainer.ServiceProvider!.GetRequiredService<IDeveloperApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IReviewApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IUserGameApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IGameApiRequest>(),
                DIContainer.ServiceProvider!.GetRequiredService<IUserApiRequest>(),
                SelectedGameID,
                userID));

            _developerApiRequest = developerApiRequest;
            _userGameApiRequest = userGameApiRequest;
            _gameApiRequest = gameApiRequest;

            GamesInfo = new ObservableCollection<Tuple<Game, Developer, bool>>();
            GameTitleClickCommand = new RelayCommand(GameTitleClick);

            SearchGames(searchText, userID);
        }

        private async void SearchGames(string searchText, int userID)
        {
            var gameRequest = await _gameApiRequest.GetGamesByTitleRequest(searchText);
            var userGameRequest = await _userGameApiRequest.GetGamesByUserIDRequest(userID);

            var games = JsonSerializer.Deserialize<ObservableCollection<Game>>(await gameRequest.Content.ReadAsStringAsync())!;
            var userGames = JsonSerializer.Deserialize<ObservableCollection<GameStats>>(await userGameRequest.Content.ReadAsStringAsync())!;

            foreach (var game in games)
            {
                bool hasEntry = userGames.Any(userGame => userGame.userID == userID && userGame.gameID == game.id);

                var developerRequest = await _developerApiRequest.GetDeveloperRequest(game.developerID);
                var dev = JsonSerializer.Deserialize<Developer>(await developerRequest.Content.ReadAsStringAsync())!;
                GamesInfo.Add(new Tuple<Game, Developer, bool>(game, dev, hasEntry)); ;
            }
        }

        public void GameTitleClick(object? parameter)
        {
            SelectedGameID = (int)parameter!;
        }
    }
}
