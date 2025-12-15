using GigSonar.Classes;

namespace GigSonar.Data;
using Microsoft.EntityFrameworkCore;

public class GigSonarContext : DbContext
{
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Venue> Venues { get; set; } = null!;
    public DbSet<Artist> Artists { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        {
            if (optionsBuilder.IsConfigured)
                return;

            // Go from bin/Debug/net9.0 → project root
            var projectRoot = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..")
            );

            var dbPath = Path.Combine(projectRoot, "GigSonarTestDataDB.db");

            optionsBuilder
                .UseSqlite($"Data Source={dbPath}")
                .LogTo(Console.WriteLine)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
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
                entity.HasOne(e => e.ArtistId).WithMany().HasForeignKey("ArtistId");
                });

        modelBuilder.Entity<Venue>(entity => 
            {
                entity.ToTable("Venue");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.ExternalId).HasMaxLength(100);
                entity.Property(e => e.Url).IsRequired(false);  
                //location relationship
                entity.HasOne(e => e.LocationData).WithMany().HasForeignKey("LocationId");
            });
        modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.ExternalId).HasMaxLength(100);
                entity.Property(e => e.SpotifyLink)
                    .IsRequired(false);
                entity.Property(e => e.FacebookLink)
                    .IsRequired(false);
                entity.Property(e => e.InstagramLink)
                    .IsRequired(false);
                entity.Property(e => e.ArtistHomepage)
                    .IsRequired(false);
            });
        modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ExternalId).HasMaxLength(100);
            }
        );
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");
            entity.HasKey(g => g.Id);
            entity.Property(g => g.ExternalId).IsRequired();
            entity.Property(g => g.Name).IsRequired();
            entity.HasOne(g => g.SubGenre)
                .WithMany()
                .HasForeignKey("SubGenreId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Subgenre");
            entity.HasKey(g => g.Id);
            entity.Property(g => g.ExternalId).IsRequired();
            entity.Property(g => g.Name).IsRequired();
        });
    }
}