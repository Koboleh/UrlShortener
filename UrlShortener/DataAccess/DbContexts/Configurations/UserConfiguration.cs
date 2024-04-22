using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.DataAccess.Entities;

namespace UrlShortener.DataAccess.DbContexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Password)
            .IsRequired();
        
        builder.Property(u => u.Username)
            .IsRequired();
        
        builder.Property(u => u.Role)
            .IsRequired();

        builder.HasMany(u => u.Urls)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId);
    }
}