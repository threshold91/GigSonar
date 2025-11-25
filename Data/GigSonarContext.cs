using GigSonar.Classes;

namespace GigSonar.Data;
using Microsoft.EntityFrameworkCore;

public class GigSonarContext : DbContext
{
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Venue> Venues { get; set; } = null!;
    public DbSet<Artist> Artists { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=GigSonarTestDataDB.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.ExternalId).HasMaxLength(100);
                //venue relationship
                entity.HasOne(e => e.Venue).WithMany().HasForeignKey("VenueId");
                });

        modelBuilder.Entity<Venue>(entity => 
            {
                entity.ToTable("Venue");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.ExternalId).HasMaxLength(100);
                //location relationship
                entity.HasOne(e => e.LocationData).WithMany().HasForeignKey("LocationId");
            });
        modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.ExternalId).HasMaxLength(100);
            });
        modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");
                entity.HasKey(e => e.Id);
            }
        );
    }
}