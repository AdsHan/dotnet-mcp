using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelContextProtocol.API.Data.Entities;

namespace ModelContextProtocol.API.Data.Mappings;

public class ColorMapping : IEntityTypeConfiguration<ColorModel>
{
    public void Configure(EntityTypeBuilder<ColorModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.HexCode)
            .IsRequired()
            .HasMaxLength(7);
    }
}
