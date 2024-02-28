using GameShopAPP.Models;
using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameShopAPP.ViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
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


        public RelayCommand PlayGameCommand { get; }
        public RelayCommand GetAchievementCommand { get; }
        public RelayCommand GameTitleClickCommand { get; }
        private readonly IUserGameApiRequest _userGameApiRequest;
        private readonly IGameStatsApiRequest _gameStatsApiRequest;
        private int _userID;

        public LibraryViewModel() { }
        public LibraryViewModel(IGameStatsApiRequest gameStatsApiRequest, IUserGameApiRequest userGameApiRequest, int userID, NavigationStore navigationStore) : base()
        {
            GameInfo = new ObservableCollection<Tuple<Game, GameStats>>();
            _userGameApiRequest = userGameApiRequest;
            _gameStatsApiRequest = gameStatsApiRequest;
            _userID = userID;
            
            PlayGameCommand = new RelayCommand(PlayGame);
            GetAchievementCommand = new RelayCommand(GetAchievement);
            GameTitleClickCommand = new RelayCommand(GameTitleClick);

            GetUserLibrary();
        }
       

        public async void GetUserLibrary()
        {
            var gameRequest = await _userGameApiRequest.GetGamesByUserIDRequest(_userID);
            var gameStatsRequest = await _gameStatsApiRequest.GetGameStatsByUserID(_userID);

            var games = JsonSerializer.Deserialize<ObservableCollection<Game>>(await gameRequest.Content.ReadAsStringAsync())!;
            var gameStats = JsonSerializer.Deserialize<ObservableCollection<GameStats>>(await gameStatsRequest.Content.ReadAsStringAsync())!;

            var tuples = games.Zip(gameStats, (game, stats) => new Tuple<Game, GameStats >(game, stats)).ToList();

            tuples.ForEach(x => GameInfo.Add(x));
        }

        public async void PlayGame(object? parameter)
        {
            var gameStats = GameInfo.FirstOrDefault(tuple => tuple.Item2.id == (int)parameter)!.Item2;
            gameStats!.hoursPlayed += 1;
            await _gameStatsApiRequest.PutGameStatsRequest((int)parameter!, gameStats); 
        }

        public async void GetAchievement(object? parameter)
        {
            var gameStats = GameInfo.FirstOrDefault(tuple => tuple.Item2.id == (int)parameter)!.Item2;
            gameStats!.achievementsGotten += 1;
            await _gameStatsApiRequest.PutGameStatsRequest((int)parameter!, gameStats);
        }

        public void GameTitleClick(object? parameter)
        {

        }
    }
}
