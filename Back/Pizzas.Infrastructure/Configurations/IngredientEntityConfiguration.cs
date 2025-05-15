using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class IngredientEntityConfiguration : IEntityTypeConfiguration<IngredientEntity>
{
    public void Configure(EntityTypeBuilder<IngredientEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasMaxLength(24);
        builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
        
        builder.HasIndex(i => i.Name)
            .IsUnique(); 
        
    }
}