using System;
using System.IO;

namespace Application.ApplicationLayer.Common
{
    public class FileManager
    {
        public static Stream Load(string fileName)
        {
            string filePath = GetPath(fileName);

            return new FileStream(filePath, FileMode.Open);
        }

        public static void Save(string fileName, Stream data)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "..", "Library");
            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, fileName + ".jpg");

            byte[] bArray = new byte[data.Length];

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }

                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
        }

        public static string GetPath(string fileName)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "..", "Library");
            string filePath = Path.Combine(directoryPath, fileName + ".jpg");

            return filePath;
        }
    }
}
