using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class DeliveryInfoEntityConfiguration : IEntityTypeConfiguration<DeliveryInfoEntity>
{
    public void Configure(EntityTypeBuilder<DeliveryInfoEntity> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id).HasMaxLength(24);
        builder.Property(d => d.UserId).HasMaxLength(24);
        builder.Property(d => d.OrderId).HasMaxLength(24);

        builder.Property(d => d.Address).IsRequired().HasMaxLength(200);  
        builder.Property(d => d.City).IsRequired().HasMaxLength(100);  
        builder.Property(d => d.PostalCode).IsRequired().HasMaxLength(20);  
        builder.Property(d => d.PhoneNumber).IsRequired().HasMaxLength(15); 
        
        builder.HasOne(d => d.User)
            .WithMany(u => u.DeliveryInfos)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(d => d.Order)
            .WithOne(o => o.DeliveryInfo)
            .HasForeignKey<DeliveryInfoEntity>(d => d.OrderId)
            .OnDelete(DeleteBehavior.Cascade);  
        
    }
}