using CarDataIngestor.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarDataIngestor.Data
{
    public class CarDataContext : DbContext
    {
        public CarDataContext()
        {

        }

        public CarDataContext(DbContextOptions<CarDataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionStringForMigration = "Server=tcp:127.0.0.1,1433;Initial Catalog=CarData;Persist Security Info=False;User Id=sa;Password=Your_password123;Connection Timeout=30";
                optionsBuilder.UseSqlServer(connectionStringForMigration);
            }
        }

        public DbSet<Drive> Drives { get; set; }
        public DbSet<DriveSnapshot> DriveSnapshots { get; set; }
    }
}