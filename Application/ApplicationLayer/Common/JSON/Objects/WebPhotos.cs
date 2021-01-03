namespace Application.ApplicationLayer.Common.JSON.Objects
{
    public class WebPhotos
    {
        public Result[] Results { get; set; }
    }

    public class Result
    {
        public Url Urls { get; set; }
    }

    public class Url
    {
        public string Full { get; set; }
    }
}
