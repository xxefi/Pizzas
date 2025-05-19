using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id).HasMaxLength(24);
        builder.Property(o => o.UserId).HasMaxLength(24);
        builder.Property(o => o.AddressId).HasMaxLength(24);
        builder.Property(o => o.TrackingNumber).HasMaxLength(24);
        builder.Property(o => o.Currency).HasMaxLength(3);
        builder.Property(o => o.Status).HasConversion<string>().IsRequired();
        builder.Property(o => o.PaymentMethod).HasConversion<string>().IsRequired();
        builder.Property(o => o.PaymentStatus).HasConversion<string>().IsRequired();

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Items)
            .WithOne(o => o.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.DeliveryInfo)
            .WithOne(o => o.Order)
            .HasForeignKey<DeliveryInfoEntity>(o => o.OrderId)  
            .OnDelete(DeleteBehavior.Cascade);
    }
}