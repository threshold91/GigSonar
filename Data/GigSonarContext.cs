using GigSonar.Classes;

namespace GigSonar.Data;
using Microsoft.EntityFrameworkCore;

public class GigSonarContext : DbContext
{
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Venue> Venues { get; set; } = null!;
    public DbSet<Artist> Artists { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=GigSonarTestDataDB.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}