using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Database;
using Domain.Database.Tables;

namespace Application.ApplicationLayer.Common.RNG
{
    public class ImageRandomizer
    {
        private readonly DbContext context;
        private int totalImageCount;
        private readonly Rng rng;

        public ImageRandomizer(DbContext context)
        {
            this.context = context;
            totalImageCount = context.GetAll<DbImage>().Count();

            rng = new Rng();
        }

        public string GetPath(int startPoint = 0)
        {
            var lastIndex = -1;

            var index = rng.GetRandomPosition(startPoint, totalImageCount, lastIndex);

            var image = context.Get<DbImage>(index);

            while(image == null)
            {
                index = rng.GetRandomPosition(startPoint, totalImageCount, lastIndex);

                image = context.Get<DbImage>(index);

                lastIndex = index;
            }

            return image.ImagePath;
        }

        public List<string> GetMultiplePaths(int amount = 0, int startPoint = 0)
        {
            var result = new List<string>();

            var totalAmount = amount == 0 ? totalImageCount : amount;

            var lastIndex = -1;

            int currentIndex;

            currentIndex = rng.GetRandomPosition(startPoint, totalAmount, lastIndex);

            for (int i = startPoint; i < totalAmount; i++)
            {
                var currentImage = context.Get<DbImage>(currentIndex);

                lastIndex = currentIndex;

                while (currentImage == null)
                {
                    currentIndex = rng.GetRandomPosition(startPoint, totalAmount, lastIndex);

                    currentImage = context.Get<DbImage>(currentIndex);

                    lastIndex = currentIndex;
                }

                result.Add(currentImage.ImagePath);

                currentIndex = rng.GetRandomPosition(startPoint, totalAmount, lastIndex);
            }

            return result;
        }
    }
}
