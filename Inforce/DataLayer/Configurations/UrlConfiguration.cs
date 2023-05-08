using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.HasKey(url => url.UrlId);
        builder.HasOne(url => url.OwnerUser)
            .WithMany(user => user.Urls)
            .HasForeignKey(url => url.OwnerUserId);
        builder
            .HasIndex(url => url.SourceUrl)
            .IsUnique();
    }
}