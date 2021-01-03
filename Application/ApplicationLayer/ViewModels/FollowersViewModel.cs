using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Application.ApplicationLayer.Services.Users;
using Domain.Database;
using Application.ApplicationLayer.DTOs.Users;
using Application.ApplicationLayer.Services.Followers;
using System.Linq;

namespace Application.ApplicationLayer.ViewModels
{
    public class FollowersViewModel : ViewModelBase
    {
        #region RelayCommands
        private RelayCommand showAllUsersCommand;
        private RelayCommand showFollowersCommand;
        private RelayCommand showFollowingCommand;
        #endregion

        #region Services
        private readonly FollowerReads followerReads;
        #endregion

        public FollowersViewModel(string userId)
        {
            UserId = userId;

            var dbContext = new DbContext();

            followerReads = new FollowerReads(dbContext);

            Users = followerReads.GetNonRelated(UserId);
        }

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

        public IEnumerable<UserMinDTO> Users
        {
            get
            {
                return users;
            }

            set
            {
                users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        private IEnumerable<UserMinDTO> users;

        public RelayCommand ShowAllUsersCommand
        {
            get
            {
                return showAllUsersCommand ??= new RelayCommand(() =>
                {
                    Users = followerReads.GetNonRelated(UserId);
                });
            }
        }

        public RelayCommand ShowFollowersCommand
        {
            get
            {
                return showFollowersCommand ??= new RelayCommand(() =>
                 {
                     Users = followerReads.GetPersonalFollowers(UserId);
                 });
            }
        }

        public RelayCommand ShowFollowingCommand
        {
            get
            {
                return showFollowingCommand ??= new RelayCommand(() =>
                {
                    Users = followerReads.GetPersonalFollowing(UserId);
                });
            }
        }
    }
}
