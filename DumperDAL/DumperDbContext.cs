using DumperDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DumperDAL
{
    public class DumperDbContext : DbContext
    {
        internal DbSet<Dumper> Dumper { get; set; }
        internal DbSet<DumperBin> DumperBin { get; set; }

        public DumperDbContext(DbContextOptions<DumperDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dumper>()
                .HasMany(dpb => dpb.DumperBins)
                .WithOne(dp => dp.Dumper);
        }
    }
}
