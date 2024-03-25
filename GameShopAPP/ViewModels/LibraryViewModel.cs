using GameShopAPP.Models;
using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;

namespace GameShopAPP.ViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
        public NavigateCommand<GameViewModel> NavigateGameCommand { get; }
        public RelayCommand PlayGameCommand { get; }
        public RelayCommand GetAchievementCommand { get; }
        public RelayCommand GameTitleClickCommand { get; }

        private readonly IUserGameApiRequest _userGameApiRequest;
        private readonly IGameStatsApiRequest _gameStatsApiRequest;

        private int _selectedGameID { get; set; }

        private ObservableCollection <Tuple<Game, GameStats>> _gameInfo;
        public ObservableCollection<Tuple<Game, GameStats>> GameInfo
        {
            get { return _gameInfo; }
            set
            {
                _gameInfo = value;
                OnPropertyChanged("GameInfo");
            }
        }

        public LibraryViewModel() { }
        public LibraryViewModel(IGameStatsApiRequest gameStatsApiRequest, IUserGameApiRequest userGameApiRequest, int userID, NavigationStore navigationStore) : base()
        {
            NavigateGameCommand = new NavigateCommand<GameViewModel>(navigationStore, () => new GameViewModel(
               DIContainer.ServiceProvider!.GetRequiredService<IDeveloperApiRequest>(),
               DIContainer.ServiceProvider!.GetRequiredService<IReviewApiRequest>(),
               DIContainer.ServiceProvider!.GetRequiredService<IUserGameApiRequest>(),
               DIContainer.ServiceProvider!.GetRequiredService<IGameApiRequest>(),
               DIContainer.ServiceProvider!.GetRequiredService<IUserApiRequest>(),
               _selectedGameID,
               userID));

            _userGameApiRequest = userGameApiRequest;
            _gameStatsApiRequest = gameStatsApiRequest;
            
            GameInfo = new ObservableCollection<Tuple<Game, GameStats>>();

            PlayGameCommand = new RelayCommand(PlayGame);
            GetAchievementCommand = new RelayCommand(GetAchievement);
            GameTitleClickCommand = new RelayCommand(GameTitleClick);

            GetUserLibrary( userID);
        }
       
        public async void GetUserLibrary(int userID)
        {
            var gameRequest = await _userGameApiRequest.GetGamesByUserIDRequest(userID);
            var gameStatsRequest = await _gameStatsApiRequest.GetGameStatsByUserIDRequest(userID);

            var games = JsonSerializer.Deserialize<ObservableCollection<Game>>(await gameRequest.Content.ReadAsStringAsync())!;
            var gameStats = JsonSerializer.Deserialize<ObservableCollection<GameStats>>(await gameStatsRequest.Content.ReadAsStringAsync())!;

            var tuples = games.Zip(gameStats, (game, stats) => new Tuple<Game, GameStats >(game, stats)).ToList();

            tuples.ForEach(x => GameInfo.Add(x));
        }

        public async void PlayGame(object? parameter)
        {
            var gameStats = GameInfo.FirstOrDefault(tuple => tuple.Item2.id == (int)parameter!)!.Item2;
            gameStats!.hoursPlayed += 1;
            await _gameStatsApiRequest.PutGameStatsRequest((int)parameter!, gameStats); 
        }

        public async void GetAchievement(object? parameter)
        {
            var gameStats = GameInfo.FirstOrDefault(tuple => tuple.Item2.id == (int)parameter!)!.Item2;
            gameStats!.achievementsGotten += 1;
            await _gameStatsApiRequest.PutGameStatsRequest((int)parameter!, gameStats);
        }

        public void GameTitleClick(object? parameter)
        {
            _selectedGameID = (int)parameter!;
        }
    }
}
