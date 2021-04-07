using Microsoft.EntityFrameworkCore;

namespace HW4._5._21.Data
{
    public class ImageDbContext : DbContext
    {
        private readonly string _connectionString;

        public ImageDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Image> Images { get; set; }
    }
}
}
