using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Configurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasMaxLength(24);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Icon).HasMaxLength(500);
        
        builder.HasMany(c => c.Pizzas)
            .WithOne(b => b.Category) 
            .HasForeignKey(b => b.CategoryId) 
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.Name);
    }
}