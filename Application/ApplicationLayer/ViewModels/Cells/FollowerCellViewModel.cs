using System;
using Application.ApplicationLayer.Services.Followers;
using Domain.Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Application.ApplicationLayer.ViewModels.Cells
{
    public class FollowerCellViewModel : ViewModelBase
    {
        #region RelayCommands
        private RelayCommand followCommand;
        #endregion

        #region Services
        private readonly FollowerWrites followerWrites;
        private readonly FollowerReads followerReads;
        #endregion

        public Action OnFollowAction;

        public FollowerCellViewModel()
        {
            var dbContext = new DbContext();

            followerWrites = new FollowerWrites(dbContext);
            followerReads = new FollowerReads(dbContext);
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

        public string TargetUserId
        {
            get
            {
                return targetUserId;
            }

            set
            {
                targetUserId = value;
                RaisePropertyChanged(nameof(TargetUserId));
            }
        }

        private string targetUserId;

        public bool UserIsFollowed
        {
            get
            {
                userIsFollowed = followerReads.UserIsFollowed(UserId, TargetUserId);
                return userIsFollowed;
            }

            set
            {
                userIsFollowed = value;
                RaisePropertyChanged(nameof(UserIsFollowed));
            }
        }

        private bool userIsFollowed;

        public RelayCommand FollowCommand
        {
            get
            {
                return followCommand ??= new RelayCommand(() =>
                {
                    if (UserIsFollowed)
                    {
                        followerWrites.UnFollow(UserId, TargetUserId);
                    }
                    else
                    {
                        followerWrites.Follow(UserId, TargetUserId);
                    }

                    OnFollowAction?.Invoke();
                });
            }
        }
    }
}
