using AnimesProtech.Domain.Entities;
using AnimesProtech.Infra.Data.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AnimesProtech.Infra.Data.Context;

public class AnimesDbContext : DbContext
{
    public Anime Anime { get; set; }
    public AnimesDbContext() { }
    public AnimesDbContext(DbContextOptions<AnimesDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AnimeMap());
    }
}