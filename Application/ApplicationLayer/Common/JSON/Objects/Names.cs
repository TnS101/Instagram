namespace Application.ApplicationLayer.Common.JSON.Objects
{
    public class Names
    {
        public UserDetails[] UserDetails { get; set; }
    }

    public class UserDetails
    {
        public string Username { get; set; }

        public string UserTag { get; set; }
    }
}
