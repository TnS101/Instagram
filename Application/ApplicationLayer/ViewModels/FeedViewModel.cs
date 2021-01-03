using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Domain.Database;
using Domain.Database.Tables;
using Application.ApplicationLayer.Services.Posts;
using System.Linq;

namespace Application.ApplicationLayer.ViewModels
{
    public class FeedViewModel : ViewModelBase
    {
        #region RelayCommands
        #endregion

        #region Services
        private readonly PostReads postReads;
        #endregion

        private readonly string userId;

        public FeedViewModel(string userId)
        {
            var dbContext = new DbContext();

            postReads = new PostReads(dbContext);

            this.userId = userId;
        }

        public List<Post> Posts
        {
            get
            {
                posts = postReads.GetRecentPosts(userId).ToList();
                posts.Reverse();

                return posts;
            }
            set
            {
                posts = value;
                RaisePropertyChanged(nameof(Posts));
            }
        }

        private List<Post> posts;
    }
}
