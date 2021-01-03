using Domain.Database;
using Domain.Database.Tables;
using Application.ApplicationLayer.Services;

namespace Application.ApplicationLayer.Services.Users
{
    public class UserWrites : ContextService
    {
        private const string defaultImageSource = "https://img.icons8.com/pastel-glyph/344/image--v1.png";

        public UserWrites(DbContext context) : base(context)
        {
        }

        public string Login(string identificationInput, string password)
        {
            var conn = Context.OpenConnection();

            User user = null;

            if (identificationInput.Contains("@"))
            {
                user = conn.Table<User>().FirstOrDefault(u => u.Email == identificationInput && u.Password == password);
            }
            else
            {
                user = conn.Table<User>().FirstOrDefault(u => u.Username == identificationInput && u.Password == password);
            }

            if (user == null) return string.Empty;

            return user.Id;
        }

        public string Logout() => string.Empty;

        public string Register(string username, string email, string password, string userTag, string imagePath)
        {
            var conn = Context.OpenConnection();

            var user = conn.Table<User>().FirstOrDefault(u => u.Username == username || u.Email == email || u.UserTag == userTag);

            if (user != null) return string.Empty;

            user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                UserTag = userTag,
                ImagePath = imagePath ??= defaultImageSource,
                Bio = string.Empty,
            };

            Context.Add(user);

            return user.Id;
        }

        public bool Delete(object id)
        {
            var user = Context.Get<User>(id);

            if (user == null) return false;

            Context.Remove<User>(id);

            return true;
        }

        public bool Update(User item)
        {
            var user = Context.Get<User>(item.Id);

            if (user == null) return false;

            if (!string.IsNullOrWhiteSpace(item.Username))
            {
                user.Username = item.Username;
            }

            if (!string.IsNullOrWhiteSpace(item.Email))
            {
                user.Email = item.Email;
            }

            if (!string.IsNullOrWhiteSpace(item.Password))
            {
                user.Password = item.Password;
            }

            if (!string.IsNullOrWhiteSpace(item.UserTag))
            {
                user.UserTag = item.UserTag;
            }

            if (!string.IsNullOrWhiteSpace(item.ImagePath))
            {
                user.ImagePath = item.ImagePath;
            }

            if (!string.IsNullOrWhiteSpace(item.Bio))
            {
                user.Bio = item.Bio;
            }

            Context.Update<User>(user);

            return true;
        }
    }
}
