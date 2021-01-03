using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Application.ApplicationLayer.Services.Users;
using Domain.Database;
using Domain.Database.Tables;
using Application.ApplicationLayer.Services.Followers;
using Application.ApplicationLayer.Services.Posts;

namespace Application.ApplicationLayer.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        #region RelayCommands
        private RelayCommand editProfileCommand;
        #endregion

        #region Services
        private readonly UserWrites userWrites;
        private readonly UserReads userReads;

        private readonly FollowerReads followerReads;

        private readonly PostReads postReads;
        #endregion

        public ProfileViewModel(string userId)
        {
            var dbContext = new DbContext();

            userWrites = new UserWrites(dbContext);
            userReads = new UserReads(dbContext);

            followerReads = new FollowerReads(dbContext);

            postReads = new PostReads(dbContext);

            UserId = userId;

            SetValues();
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

        public string Bio
        {
            get
            {
                return bio;
            }

            set
            {
                bio = value;
                RaisePropertyChanged(nameof(Bio));
            }
        }

        private string bio;

        public int FollowersCount
        {
            get
            {
                followersCount = followerReads.GetPersonalFollowers(UserId).Count();
                return followersCount;
            }

            set
            {
                followersCount = value;
                RaisePropertyChanged(nameof(FollowersCount));
            }
        }

        private int followersCount;

        public int FollowingCount
        {
            get
            {
                followingCount = followerReads.GetPersonalFollowing(UserId).Count();
                return followingCount;
            }

            set
            {
                followingCount = value;
                RaisePropertyChanged(nameof(FollowingCount));
            }
        }

        private int followingCount;

        public IEnumerable<Post> Posts
        {
            get
            {
                posts = postReads.GetAll(UserId).Reverse();
                return posts;
            }

            set
            {
                posts = value;
                RaisePropertyChanged(nameof(Posts));
            }
        }

        private IEnumerable<Post> posts;

        public int PostsCount
        {
            get
            {
                postsCount = Posts.Count();
                return postsCount;
            }

            set
            {
                postsCount = value;
                RaisePropertyChanged(nameof(PostsCount));
            }
        }

        private int postsCount;

        public RelayCommand EditProfileCommand
        {
            get
            {
                return editProfileCommand ??= new RelayCommand(() =>
                 {
                     var updatedUser = new User
                     {
                         Id = UserId,
                         Username = Username,
                         Email = string.Empty,
                         Password = string.Empty,
                         Bio = Bio,
                         UserTag = UserTag,
                         ImagePath = ImagePath,
                     };

                     userWrites.Update(updatedUser);
                 });
            }
        }

        private void SetValues()
        {
            var user = userReads.Get(UserId);

            Username = user.Username;
            UserTag = user.UserTag;
            ImagePath = user.ImagePath;
            Bio = user.Bio;
        }
    }
}
