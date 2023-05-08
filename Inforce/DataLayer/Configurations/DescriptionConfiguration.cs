using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class DescriptionConfiguration : IEntityTypeConfiguration<Description>
{
    public void Configure(EntityTypeBuilder<Description> builder)
    {
        builder.HasData(new List<Description>()
        {
            new Description(1, "Algorithm calculates int id for url and uses as part of short url.")
        });
    }
}