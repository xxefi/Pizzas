using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class FavoriteEntityConfiguration : IEntityTypeConfiguration<FavoriteEntity>
{
    public void Configure(EntityTypeBuilder<FavoriteEntity> builder)
    {
        builder.HasKey(f => f.Id);
        
        builder.Property(f => f.Id)
            .HasMaxLength(24)
            .IsRequired();
        builder.Property(f => f.UserId)
            .HasMaxLength(24)  
            .IsRequired();
        builder.Property(f => f.PizzaId)
            .HasMaxLength(24) 
            .IsRequired();
        builder.Property(f => f.AddedAt)
            .IsRequired();
        builder.Property(f => f.RemovedAt)
            .IsRequired(false);
        builder.Property(f => f.IsActive)
            .IsRequired();
        
        builder.HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.Pizza)
            .WithMany()
            .HasForeignKey(f => f.PizzaId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(f => f.UserId)
            .IsUnique();
    }
}