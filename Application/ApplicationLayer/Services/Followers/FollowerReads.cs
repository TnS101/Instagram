using System.Collections.Generic;
using System.Linq;
using Domain.Database;
using Application.ApplicationLayer.DTOs.Users;
using Domain.Database.Tables;
using SQLite;

namespace Application.ApplicationLayer.Services.Followers
{
    public class FollowerReads : ContextService
    {
        public FollowerReads(DbContext context) : base(context)
        {
        }

        public IEnumerable<UserMinDTO> GetPersonalFollowing(string userId)
        {
            var users = Context.GetAll<User>();
            var following = Context.GetAll<Follower>().Where(f => f.FollowerId == userId);

            return (IEnumerable<UserMinDTO>)from follower in following
                                            join user in users on follower.UserId equals user.Id
                                            select new UserMinDTO { Id = user.Id, Username = user.Username, ImagePath = user.ImagePath, UserTag = user.UserTag };
        }

        public IEnumerable<UserMinDTO> GetPersonalFollowers(string userId)
        {
            var users = Context.GetAll<User>();
            var followers = Context.GetAll<Follower>().Where(f => f.UserId == userId);

            return (IEnumerable<UserMinDTO>)from follower in followers
                                                   join user in users on follower.FollowerId equals user.Id
                                                   select new UserMinDTO { Id = user.Id, Username = user.Username, ImagePath = user.ImagePath, UserTag = user.UserTag };
        }

        public IEnumerable<UserMinDTO> GetNonRelated(string userId)
        {
            var users = Context.GetAll<User>().Where(u => u.Id != userId).ToList();
            
            var related = Context.GetAll<Follower>().Where(f => f.UserId == userId || f.FollowerId == userId);

            foreach (var relatedUser in related)
            {
                var user = users.FirstOrDefault(u => u.Id == relatedUser.UserId || u.Id == relatedUser.FollowerId);

                if (user != null)
                {
                    users.Remove(user);
                }
            }

            return users.Select(u => new UserMinDTO
            {
                Id = u.Id,
                Username = u.Username,
                UserTag = u.UserTag,
                ImagePath = u.ImagePath,
            });
        }

        public bool UserIsFollowed(string followerId, string targetId) => Context.GetAll<Follower>().Any(f => f.FollowerId == followerId && f.UserId == targetId);
    }
}
