using Hahaton.Core.Models;
using Hahaton.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Hahaton.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Mission> Missions => Set<Mission>();
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MissionConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

