using System.Collections.Generic;
using Application.ApplicationLayer.DTOs.Users;
using Domain.Database;
using Domain.Database.Tables;
using System.Linq;

namespace Application.ApplicationLayer.Services.Users
{
    public class UserReads : ContextService
    {
        public UserReads(DbContext context) : base(context)
        {
        }

        public User Get(string id)
        {
            var user = Context.Get<User>(id);

            return user;
        }

        public UserProfileDTO GetUserProfile(string id)
        {
            var user = Context.Get<User>(id);

            return new UserProfileDTO
            {
                Id = user.Id,
                Username = user.Username,
                Bio = user.Bio,
                ImagePath = user.ImagePath,
                UserTag = user.UserTag,
            };
        }

        public IEnumerable<User> GetAll()
        {
            var users = Context.GetAll<User>();

            return users;
        }

        public IEnumerable<UserMinDTO> GetAllMin()
        {
            var users = Context.GetAll<User>();

            return users.Select(u => new UserMinDTO
            {
                Id = u.Id,
                Username = u.Username,
                UserTag = u.UserTag,
                ImagePath = u.ImagePath,
            });
        }
    }
}
