using Domain.Database;
using Domain.Database.Tables;

namespace Application.ApplicationLayer.Services.Followers
{
    public class FollowerWrites : ContextService
    {
        public FollowerWrites(DbContext context) : base(context)
        {
        }

        public bool Follow(string userId, string targetUserId)
        {
            if (UsersAreNull(userId, targetUserId)) return false;

            Context.Add(new Follower
            {
                UserId = targetUserId,
                FollowerId = userId,
            });

            return true;
        }

        public bool UnFollow(string userId, string targetUserId)
        {
            if (UsersAreNull(userId, targetUserId)) return false;

            var follower = Context.GetAll<Follower>().FirstOrDefault(f => f.UserId == targetUserId &&  f.FollowerId == userId);

            Context.Remove(follower);

            return true;
        }

        private bool UsersAreNull(string userId, string targetUserId)
        {
            var targetUser = Context.Get<User>(targetUserId);
            var follower = Context.Get<User>(userId);

            if (targetUser == null || follower == null) return true;

            return false;
        }
    }
}
