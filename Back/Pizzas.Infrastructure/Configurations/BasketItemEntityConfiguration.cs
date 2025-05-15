using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class BasketItemEntityConfiguration : IEntityTypeConfiguration<BasketItemEntity>
{
    public void Configure(EntityTypeBuilder<BasketItemEntity> builder)
    {
        builder.HasKey(bi => bi.Id);

        builder.Property(bi => bi.Id).HasMaxLength(24);
        builder.Property(bi => bi.PizzaId).HasMaxLength(24);
        builder.Property(bi => bi.BasketId).HasMaxLength(24);
        
        builder.Property(bi => bi.Quantity).IsRequired();

        builder.HasOne(bi => bi.Pizza)
            .WithMany() 
            .HasForeignKey(bi => bi.PizzaId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(bi => bi.Basket)
            .WithMany(b => b.Items)  
            .HasForeignKey(bi => bi.BasketId) 
            .OnDelete(DeleteBehavior.Cascade); 
    }
}