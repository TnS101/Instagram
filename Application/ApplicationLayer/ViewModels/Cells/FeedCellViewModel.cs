using System;
using Application.ApplicationLayer.Services.Posts;
using Domain.Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Application.ApplicationLayer.ViewModels.Cells
{
    public class FeedCellViewModel : ViewModelBase
    {
        public Action DeleteItemAction;
        public Action LikeAction;

        #region RelayCommands
        private RelayCommand likePostCommand;
        private RelayCommand deletePostCommand;
        #endregion

        #region Services
        private readonly PostWrites postWrites;
        #endregion

        public FeedCellViewModel()
        {
            var dbContext = new DbContext();

            postWrites = new PostWrites(dbContext);
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

        public string AuthorId
        {
            get
            {
                return authorId;
            }

            set
            {
                authorId = value;
                RaisePropertyChanged(nameof(AuthorId));
            }
        }

        private string authorId;

        public string PostId
        {
            get
            {
                return postId;
            }

            set
            {
                postId = value;
                RaisePropertyChanged(nameof(PostId));
            }
        }

        private string postId;

        public RelayCommand DeletePostCommand
        {
            get
            {
                return deletePostCommand ??= new RelayCommand(() =>
                {
                    postWrites.Delete(PostId);
                    DeleteItemAction?.Invoke();
                });
            }
        }

        public RelayCommand LikePostCommand
        {
            get
            {
                return likePostCommand ??= new RelayCommand(() =>
                {
                    postWrites.LikePost(PostId, UserId);
                    LikeAction?.Invoke();
                });
            }
        }
    }
}
