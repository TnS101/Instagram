using SQLite;

namespace Domain.Database.Tables
{
    [Table("DbImages")]
    public class DbImage
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        public string ImagePath { get; set; }
    }
}
