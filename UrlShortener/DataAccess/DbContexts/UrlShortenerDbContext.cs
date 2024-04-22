using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess.DbContexts.Configurations;
using UrlShortener.DataAccess.Entities;
using UrlShortener.DataAccess.Entities.Enums;

namespace UrlShortener.DataAccess.DbContexts;

public class UrlShortenerDbContext : DbContext
{
    public virtual DbSet<User?> Users { get; set; }
    public virtual DbSet<Url> Urls { get; set; }

    public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UrlConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());

        SeedUserData(builder);
    }
    
    private void SeedUserData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "ordinaryUser",
                Password = "$2a$12$mBXrsuF73vv6qFovuE8tEOvBhABgHgcvgoTJU8lHoY3UgMgT7O622",
                Role = Role.User
            },
            new User
            {
                Id = 2,
                Username = "defaultUser",
                Password = "$2a$12$mBXrsuF73vv6qFovuE8tEOvBhABgHgcvgoTJU8lHoY3UgMgT7O622",
                Role = Role.User
            },
            new User
            {
                Id = 3,
                Username = "adminUser",
                Password = "$2a$12$mBXrsuF73vv6qFovuE8tEOvBhABgHgcvgoTJU8lHoY3UgMgT7O622",
                Role = Role.Admin
            }
        );
    }
}