using Microsoft.EntityFrameworkCore;

namespace TextP.Models
{
    public class DBWords : DbContext
    {
        public DbSet<Word> Words { get; set; }

        public DBWords(DbContextOptions<DBWords> options): base(options) {}

        public void CreateNewBD()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public bool DeleteDB()
        {
            if (Database.CanConnect())
            {
                Database.EnsureDeleted();
                return true;
            }
            return false;
        }
    }
}
