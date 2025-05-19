using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class BasketEntityConfiguration : IEntityTypeConfiguration<BasketEntity>
{
    public void Configure(EntityTypeBuilder<BasketEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasMaxLength(24);
        builder.Property(b => b.UserId).HasMaxLength(24);

        builder.HasOne(b => b.User)
            .WithOne() 
            .HasForeignKey<BasketEntity>(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(b => b.Items)
            .WithOne(bi => bi.Basket) 
            .HasForeignKey(bi => bi.BasketId)
            .OnDelete(DeleteBehavior.Cascade); 
        
    }
}