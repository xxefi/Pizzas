using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities.Main;
using Pizzas.Core.Enums;

namespace Pizzas.Infrastructure.Configurations;

public class PizzaPriceEntityConfiguration : IEntityTypeConfiguration<PizzaPriceEntity>
{
    public void Configure(EntityTypeBuilder<PizzaPriceEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasMaxLength(24);
        builder.Property(p => p.PizzaId).HasMaxLength(24);
        builder.Property(p => p.OriginalPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(p => p.DiscountPrice).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(p => p.Size)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (PizzaSize)Enum.Parse(typeof(PizzaSize), v)
            );

        builder.HasOne(p => p.Pizza)
            .WithMany(p => p.Prices)
            .HasForeignKey(p => p.PizzaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}