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
                optionsBuilder.UseSqlServer("Server=tcp:aevansobddata.database.windows.net,1433;Initial Catalog=cardata;Persist Security Info=False;User ID=obd-data-user;Password=e1b866e4-4f11-42d9-a640-ffa7c453c73c;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        public DbSet<Drive> Drives { get; set; }
        public DbSet<DriveSnapshot> DriveSnapshots { get; set; }
    }
}