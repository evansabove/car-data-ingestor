using CarDataIngestor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
                var connectionStringForMigration = "";
                optionsBuilder.UseSqlServer(connectionStringForMigration);
            }
        }

        public DbSet<Drive> Drives { get; set; }
        public DbSet<DriveSnapshot> DriveSnapshots { get; set; }
    }
}