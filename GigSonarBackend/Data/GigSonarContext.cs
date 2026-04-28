using GigSonarBackend.Classes;
using GigSonarBackend.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigSonarBackend.Data;

public class GigSonarContext : IdentityDbContext<ApplicationUser>
{
    public GigSonarContext(DbContextOptions<GigSonarContext> options)
        : base(options)
    {
    }
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Venue> Venues { get; set; } = null!;
    public DbSet<Artist> Artists { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<SubGenre> SubGenres { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
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