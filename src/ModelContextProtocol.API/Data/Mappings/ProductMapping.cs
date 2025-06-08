using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelContextProtocol.API.Data.Entities;

namespace ModelContextProtocol.API.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Price)
            .HasPrecision(10, 2);

        builder
            .HasMany(p => p.Colors)
            .WithMany();
    }
}
