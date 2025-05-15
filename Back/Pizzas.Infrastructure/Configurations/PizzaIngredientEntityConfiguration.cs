using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class PizzaIngredientEntityConfiguration : IEntityTypeConfiguration<PizzaIngredientEntity>
{
    public void Configure(EntityTypeBuilder<PizzaIngredientEntity> builder)
    {
        builder.HasKey(pi => new { pi.PizzaId, pi.IngredientId });

        builder.HasOne(pi => pi.Pizza)
            .WithMany(p => p.PizzaIngredients)
            .HasForeignKey(pi => pi.PizzaId);

        builder.HasOne(pi => pi.Ingredient)
            .WithMany(i => i.PizzaIngredients)
            .HasForeignKey(pi => pi.IngredientId);

        builder.Property(pi => pi.Quantity).IsRequired();
    }
}