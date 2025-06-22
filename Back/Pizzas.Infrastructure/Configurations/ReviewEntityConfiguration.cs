using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class ReviewEntityConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).HasMaxLength(24);
        builder.Property(r => r.UserId).HasMaxLength(24);
        builder.Property(r => r.PizzaId).HasMaxLength(24);
        builder.Property(r => r.Content).IsRequired().HasMaxLength(1000);
        builder.Property(r => r.Rating).IsRequired().HasDefaultValue(0);
        builder.Property(r => r.UserId).IsRequired();
        builder.Property(r => r.PizzaId).IsRequired();

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Pizza)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.PizzaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}