using System;
using SQLite;

namespace Domain.Database.Tables
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        [PrimaryKey, Column("id")]
        public string Id { get; set; }

        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        [MaxLength(20)]
        public string UserTag { get; set; }

        public string ImagePath { get; set; }

        [MaxLength(100)]
        public string Bio { get; set; }
    }
}
