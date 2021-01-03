using System;
namespace Application.ApplicationLayer.Common.RNG
{
    public class Rng
    {
        private readonly Random rng;

        public Rng()
        {
            rng = new Random();
        }

        public int GetRandomPosition(int startPoint, int endPoint, int lastPosition)
        {
            var result = rng.Next(startPoint, endPoint - 1);

            if (lastPosition != -1)
            {
                while (result == lastPosition)
                {
                    result = rng.Next(startPoint, endPoint);
                }
            }

            return result;
        }
    }
}
