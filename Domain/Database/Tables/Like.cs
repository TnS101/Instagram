using SQLite;

namespace Domain.Database.Tables
{
    [Table("Likes")]
    public class Like
    {
        public string UserId { get; set; }

        public string PostId { get; set; }
    }
}
