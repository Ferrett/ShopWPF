﻿using GameShopAPP.Models;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace GameShopAPP.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public RelayCommand BuyGameCommand { get; }

        private readonly IDeveloperApiRequest _developerApiRequest;
        private readonly IReviewApiRequest _reviewApiRequest;
        private readonly IUserGameApiRequest _userGameApiRequest;
        private readonly IGameApiRequest _gameApiRequest;
        private readonly IUserApiRequest _userApiRequest;

        private int _gameID;
        private int _userID;

        private Game _game;
        public Game Game
        {
            get { return _game; }
            set
            {
                _game = value;
                OnPropertyChanged("Game");
            }
        }

        private Developer _developer;
        public Developer Developer
        {
            get { return _developer; }
            set
            {
                _developer = value;
                OnPropertyChanged("Developer");
            }
        }

        private ObservableCollection<Tuple<Review, User>> _reviewsInfo;
        public ObservableCollection<Tuple<Review, User>> ReviewsInfo
        {
            get { return _reviewsInfo; }
            set
            {
                _reviewsInfo = value;
                OnPropertyChanged("ReviewsInfo");
            }
        }

        private double _averageRating;
        public double AverageRating
        {
            get { return _averageRating; }
            set
            {
                _averageRating = value;
                OnPropertyChanged("AverageRating");
            }
        }

        private bool _isGameBought;
        public bool IsGameBought
        {
            get { return _isGameBought; }
            set
            {
                _isGameBought = value;
                OnPropertyChanged("IsGameBought");
            }
        }

        public GameViewModel() { }
        public GameViewModel(IDeveloperApiRequest developerApiRequest, IReviewApiRequest reviewApiRequest, IUserGameApiRequest userGameApiRequest, IGameApiRequest gameApiRequest, IUserApiRequest userApiRequest, int gameID, int userID)
        {
            _developerApiRequest = developerApiRequest;
            _reviewApiRequest = reviewApiRequest;
            _userGameApiRequest = userGameApiRequest;
            _gameApiRequest = gameApiRequest;
            _userApiRequest = userApiRequest;

            _gameID = gameID;
            _userID = userID;
            ReviewsInfo = new ObservableCollection<Tuple<Review, User>>();
            IsGameBought = false;

            BuyGameCommand = new RelayCommand(BuyGame);
            Test();
        }

        private async void Test()
        {
            await GetGame();
            await Task.WhenAll(GetDeveloper(), GetReviews(), GetGameBought());

            GetAverageRating();
        }

        private async Task GetGameBought()
        {
            var gameRequest = await _userGameApiRequest.GetGamesByUserIDRequest(_userID);
            var games = JsonSerializer.Deserialize<ObservableCollection<Game>>(await gameRequest.Content.ReadAsStringAsync())!;

            if(games.Any(x=>x.id==_gameID))
                IsGameBought = true;
        }
        private async Task GetGame()
        {
            var gameRequest = await _gameApiRequest.GetGameRequest(_gameID);

            Game = JsonSerializer.Deserialize<Game>(await gameRequest.Content.ReadAsStringAsync())!;
        }

        private async Task GetReviews()
        {
            var reviewRequest = await _reviewApiRequest.GetReviewsByGameIDRequest(Game.id);
            var reviews = JsonSerializer.Deserialize<ObservableCollection<Review>>(await reviewRequest.Content.ReadAsStringAsync())!;

            foreach (var review in reviews)
            {
                var userRequest = await _userApiRequest.GetUserRequest(review.userID);
                var user = JsonSerializer.Deserialize<User>(await userRequest.Content.ReadAsStringAsync())!;

                ReviewsInfo.Add(new Tuple<Review, User>(review, user));
            }
        }

        private void GetAverageRating()
        {
            if (ReviewsInfo.Count <= 0)
                return;

            double positiveReviews = 0;
            
            foreach (var tuple in ReviewsInfo)
            {
                Review review = tuple.Item1;
                if (review.isPositive)
                {
                    positiveReviews++;
                }
            }
            AverageRating = (positiveReviews / ReviewsInfo.Count) * 100.0;
        }

        private async Task GetDeveloper()
        {
            var developerRequest = await _developerApiRequest.GetDeveloperRequest(Game.developerID);

            Developer = JsonSerializer.Deserialize<Developer>(await developerRequest.Content.ReadAsStringAsync())!;
        }

        public async void BuyGame(object? parameter)
        {
            IsGameBought = false;
            await _userGameApiRequest.PostUserGameRequest(new UserGame() { gameID = _gameID, userID = _userID });
        }
    }
}
