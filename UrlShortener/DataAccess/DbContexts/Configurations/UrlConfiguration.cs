using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.DataAccess.Entities;

namespace UrlShortener.DataAccess.DbContexts.Configurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(u => u.Name)
            .IsRequired();

        builder.Property(u => u.ShortUrl)
            .IsRequired();
        
        builder.Property(u => u.OriginalUrl)
            .IsRequired();
        
        builder.Property(u => u.CreatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");
    }
}