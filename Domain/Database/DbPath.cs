using System;
using System.IO;

namespace Domain.Database
{
    public class DbPath
    {
        public static string String()
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentPath, "..", "Library");

            return Path.Combine(libraryPath, "Instagram.db3");
        }
    }
}
