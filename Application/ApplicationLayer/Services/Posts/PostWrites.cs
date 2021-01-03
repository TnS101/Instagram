using System;
using System.Linq;
using Application.ApplicationLayer.Common.RNG;
using Domain.Database;
using Domain.Database.Tables;

namespace Application.ApplicationLayer.Services.Posts
{
    public class PostWrites : ContextService
    {
        private readonly ImageRandomizer imageRandomizer;

        public PostWrites(DbContext context) : base(context)
        {
            imageRandomizer = new ImageRandomizer(context);
        }

        public bool LikePost(string postId, string userId)
        {
            var post = Context.Get<Post>(postId);

            if (post == null) return false;

            if (Context.GetAll<Like>().Any(l => l.PostId == postId && l.UserId == userId)) return false;

            Context.Add<Like>(new Like { PostId = postId, UserId = userId });

            post.Likes++;

            Context.Update<Post>(post);

            return true;
        }

        public bool Create(string userId, string description, string imagePath)
        {
            bool usesURL = false;

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                imagePath = imageRandomizer.GetPath();
                usesURL = true;
            }

            if (userId == null || description == null || imagePath == null) return false;

            Context.Add(new Post
            {
                UserId = userId,
                Description = description,
                ImagePath = imagePath,
                Likes = 0,
                PostedOn = DateTime.UtcNow.ToShortDateString(),
                UsesURL = usesURL,
            });

            return true;
        }

        public bool Delete(string id)
        {
            if (!Context.GetAll<Post>().Any(p => p.Id == id)) return false;

            Context.Remove<Post>(id);

            return true;
        }

        public bool Update(Post item)
        {
            var post = Context.Get<Post>(item.Id);

            if (post == null) return false;

            if (!string.IsNullOrWhiteSpace(item.Description))
            {
                post.Description = item.Description;
            }

            if (!string.IsNullOrWhiteSpace(item.ImagePath))
            {
                post.ImagePath = item.ImagePath;
            }

            Context.Update<Post>(post);

            return true;
        }
    }
}
