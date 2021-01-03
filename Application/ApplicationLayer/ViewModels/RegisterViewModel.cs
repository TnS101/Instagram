using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Application.ApplicationLayer.Services.Users;
using Domain.Database;

namespace Application.ApplicationLayer.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        #region RelayCommands
        private RelayCommand<string> registerCommand;
        private RelayCommand addImageCommand;
        #endregion

        #region
        private readonly UserWrites userWrites;
        #endregion

        public RegisterViewModel()
        {
            userWrites = new UserWrites(new DbContext());
        }

        public bool RegisterSucceeded
        {
            get
            {
                return registerSucceeded;
            }

            set
            {
                registerSucceeded = value;
                RaisePropertyChanged(nameof(RegisterSucceeded));
            }
        }

        private bool registerSucceeded;

        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
                RaisePropertyChanged(nameof(UserId));
            }
        }

        private string userId;

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        private string username;

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        private string password;

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        private string email;

        public string UserTag
        {
            get
            {
                return userTag;
            }

            set
            {
                userTag = value;
                RaisePropertyChanged(nameof(UserTag));
            }
        }

        private string userTag;

        public string ImagePath
        {
            get
            {
                return imagePath;
            }

            set
            {
                imagePath = value;
                RaisePropertyChanged(nameof(ImagePath));
            }
        }

        private string imagePath;

        public RelayCommand AddImageCommand
        {
            get
            {
                return addImageCommand ??= new RelayCommand(() =>
                {
                });
            }
        }

        public RelayCommand<string> RegisterCommand
        {
            get
            {
                return registerCommand ??= new RelayCommand<string>((fields) =>
                {
                    var result = userWrites.Register(Username, Email, Password, UserTag, ImagePath);

                    if (result != string.Empty)
                    {
                        UserId = result;
                        RegisterSucceeded = true;
                    }
                    else
                    {
                        RegisterSucceeded = false;
                    }
                }, fields => !string.IsNullOrWhiteSpace(fields));
            }
        }
    }
}
