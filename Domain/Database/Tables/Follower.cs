using System;
using SQLite;

namespace Domain.Database.Tables
{
    [Table("Followers")]
    public class Follower
    {
        public Follower()
        {
            Id = Guid.NewGuid().ToString();
        }

        [PrimaryKey, Column("id")]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FollowerId { get; set; }
    }
}
