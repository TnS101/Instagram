using System.Collections.Generic;
using System.Linq;
using Domain.Database;
using Domain.Database.Tables;

namespace Application.ApplicationLayer.Services.Posts
{
    public class PostReads : ContextService
    {
        public PostReads(DbContext context) : base(context)
        {
        }

        public IEnumerable<Post> GetRecentPosts(string userId, int amount = 50)
        {
            var following = Context.GetAll<Follower>().Where(f => f.FollowerId == userId);

            var posts = Context.GetAll<Post>().Take(amount);

            var resultPosts = from follower in following
                                 join post in posts on follower.UserId equals post.UserId
                                 select new Post { Id = post.Id, UserId = post.UserId, Description = post.Description,
                                 ImagePath = post.ImagePath, Likes = post.Likes, PostedOn = post.PostedOn };

            var yourPosts = posts.Where(p => p.UserId == userId).ToList();
            
            return resultPosts.Concat(yourPosts);
        }

        public Post Get(string id)
        {
            var post = Context.Get<Post>(id);

            return post;
        }

        public IEnumerable<Post> GetAll(string userId)
        {
            IEnumerable<Post> posts;

            if (userId != string.Empty)
            {
                posts = Context.GetAll<Post>().Where(p => p.UserId == userId);
            }
            else
            {
                posts = Context.GetAll<Post>();
            }

            return posts;
        }
    }
}
