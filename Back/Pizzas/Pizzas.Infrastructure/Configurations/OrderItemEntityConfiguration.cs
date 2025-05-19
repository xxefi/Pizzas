using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id).HasMaxLength(24);
        builder.Property(oi => oi.OrderId).HasMaxLength(24);
        builder.Property(oi => oi.PizzaId).HasMaxLength(24);
        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.Price).IsRequired().HasColumnType("decimal(18,2)");  
        
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(oi => oi.Pizza)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.PizzaId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}