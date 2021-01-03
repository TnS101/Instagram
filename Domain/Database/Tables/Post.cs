namespace Domain.Database.Tables
{
    using System;
    using SQLite;

    [Table("Posts")]
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid().ToString();
        }

        [PrimaryKey, Column("id")]
        public string Id { get; set; }

        public string UserId { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public int Likes { get; set; }

        [MaxLength(200)]
        public string PostedOn { get; set; }

        public bool UsesURL { get; set; }
    }
}
