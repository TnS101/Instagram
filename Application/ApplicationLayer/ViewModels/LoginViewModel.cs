using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Application.ApplicationLayer.Services.Users;
using Domain.Database;

namespace Application.ApplicationLayer.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region RelayCommands
        private RelayCommand<string> loginCommand;
        #endregion

        #region Services
        private readonly UserWrites userWrites;
        #endregion

        public LoginViewModel()
        {
            userWrites = new UserWrites(new DbContext());
        }

        public bool LoginSucceeded
        {
            get
            {
                return loginSucceeded;
            }

            set
            {
                loginSucceeded = value;
                RaisePropertyChanged(nameof(LoginSucceeded));
            }
        }

        private bool loginSucceeded;

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

        public RelayCommand<string> LoginCommand
        {
            get
            {
                return loginCommand ??= new RelayCommand<string>(fields =>
                {
                    var result = userWrites.Login(Username, Password);

                    if (result != string.Empty)
                    {
                        UserId = result;
                        LoginSucceeded = true;
                    }
                    else
                    {
                        LoginSucceeded = false;
                    }

                }, fields => !string.IsNullOrWhiteSpace(fields));
            }
        }
    }
}
