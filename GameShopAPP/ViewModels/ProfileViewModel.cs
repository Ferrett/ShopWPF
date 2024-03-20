using GameShopAPP.Models;
using GameShopAPP.Models.ServiceModels;
using GameShopAPP.Services;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using Microsoft.Win32;
using System;
using System.Text.Json;
using System.Windows.Media.Imaging;

namespace GameShopAPP.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public RelayCommand SaveChangesCommand { get; }
        public RelayCommand ChangePFPCommand { get; }

        private readonly IUserApiRequest _userApiRequest;
        private readonly IRegistrationModelValidation _registrationModelValidation;

        private int _userID;

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

        private bool _isChangesSuccessful;
        public bool IsChangesSuccessful
        {
            get { return _isChangesSuccessful; }
            set
            {
                _isChangesSuccessful = value;
                OnPropertyChanged("IsChangesSuccessful");
            }
        }

        private string _responseText;
        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                _responseText = value;
                OnPropertyChanged("ResponseText");
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }

        private BitmapImage _openedImage;
        public BitmapImage OpenedImage
        {
            get { return _openedImage; }
            set
            {
                _openedImage = value;
                OnPropertyChanged("OpenedImage");
            }
        }

        public ProfileViewModel(IUserApiRequest userApiRequest, IRegistrationModelValidation registrationModelValidation, int userID)
        {
            _userApiRequest = userApiRequest;
            _registrationModelValidation = registrationModelValidation;
            _userID = userID;

            IsChangesSuccessful = false;
            ResponseText = string.Empty;
            NewPassword = string.Empty;

            SaveChangesCommand = new RelayCommand(SaveChanges);
            ChangePFPCommand = new RelayCommand(ChangePFP);

            GetUser(_userID);
        }

        private async void GetUser(int userID)
        {
            var usereRequest = await _userApiRequest.GetUserRequest(userID);

            User = JsonSerializer.Deserialize<User>(await usereRequest.Content.ReadAsStringAsync())!;
        }

        public void ChangePFP(object? parameter)
        {
            OpenFileDialog openFileDialogLoad = new OpenFileDialog();

            if (openFileDialogLoad.ShowDialog() == true)
            {
                OpenedImage = new BitmapImage(new Uri(openFileDialogLoad.FileName));
                User.profilePictureURL = OpenedImage.UriSource.OriginalString;
            }
        }

        public async void SaveChanges(object? parameter)
        {
            var validationResult = _registrationModelValidation.Validate(new RegistrationModel()
            {
                login = User.login,
                password = User.password,
                nickname = User.nickname,
                email = User.email
            });

            if (validationResult.result == false)
            {
                IsChangesSuccessful = false;
                ResponseText = validationResult.errorMessage;
                return;
            }
            else
            {
                IsChangesSuccessful = true;
                ResponseText = "Data updated successfully";
            }

            User.password = BCrypt.Net.BCrypt.HashPassword(User.password);
            User.email = User.email == string.Empty ? null : User.email;

            await _userApiRequest.PutUserRequest(_userID, User);
            await _userApiRequest.PutUserLogoRequest(_userID, OpenedImage);
        }
    }
}
