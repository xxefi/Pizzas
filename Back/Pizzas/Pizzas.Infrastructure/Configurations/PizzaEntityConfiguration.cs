using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Core.Enums;

namespace Pizzas.Infrastructure.Configurations;

public class PizzaEntityConfiguration : IEntityTypeConfiguration<PizzaEntity>
{
    public void Configure(EntityTypeBuilder<PizzaEntity> builder)
    {
        builder.HasKey(p => p.Id);
       
        builder.Property(p => p.Id).HasMaxLength(24);
        builder.Property(p => p.CategoryId).HasMaxLength(24);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.ImageUrl).IsRequired().HasMaxLength(500);
        
        builder.Property(p => p.Size)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (PizzaSize)Enum.Parse(typeof(PizzaSize), v)
            );
        

        builder.HasMany(p => p.Prices)
            .WithOne(price => price.Pizza)
            .HasForeignKey(price => price.PizzaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Pizza)
            .HasForeignKey(r => r.PizzaId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}