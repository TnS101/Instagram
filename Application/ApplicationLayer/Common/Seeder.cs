using System.IO;
using System.Linq;
using Application.ApplicationLayer.Common.JSON.Objects;
using Domain.Database;
using Domain.Database.Tables;
using Newtonsoft.Json;

namespace Application.ApplicationLayer.Common
{
    public class Seeder
    {
        public static void SeedImages(DbContext context)
        {
            var imageResponse = WebCaller.Call("https://unsplash.com/napi/search?query=cat&per_page=20&xp=feedback-loop-v2%3");

            var images = JsonConvert.DeserializeObject<WebPhotos>(imageResponse);

            context.AddMany(images.Results.Select(p => new DbImage
            {
                ImagePath = p.Urls.Full
            }));
        }

        public static void SeedUsers(DbContext context)
        {
            var currentPage = 2;

            var imageResponse = WebCaller.Call($"https://unsplash.com/napi/search/photos?query=face&per_page=20&page={currentPage}&xp=fee");

            var images = JsonConvert.DeserializeObject<WebPhotos>(imageResponse);

            var userDetailsJSON = File.ReadAllText(Path.GetFullPath("/Users/hristotodorov/Projects/Instagram/Application/ApplicationLayer/Common/JSON/Files/Names.json"));

            var userDetails = JsonConvert.DeserializeObject<Names>(userDetailsJSON);

            for (int i = 0; i < userDetails.UserDetails.Length; i++)
            {
                var userDetail = userDetails.UserDetails[i];
                string userImagePath;

                if (i > images.Results.Length - 1)
                {
                    currentPage++;

                    imageResponse = WebCaller.Call($"https://unsplash.com/napi/search/photos?query=face&per_page=20&page={currentPage}&xp=fee");
                    images = JsonConvert.DeserializeObject<WebPhotos>(imageResponse);

                    userImagePath = images.Results[i - images.Results.Length].Urls.Full;
                }
                else
                {
                    userImagePath = images.Results[i].Urls.Full;
                }

                var user = new User()
                {
                    Username = userDetail.Username,
                    UserTag = userDetail.UserTag,
                    ImagePath = userImagePath,
                    Bio = "Random Bio",
                    Email = "email@email.com",
                    Password = "123"
                };

                user.Id += i.ToString();

                context.Add(user);
            }
        }
    }
}
