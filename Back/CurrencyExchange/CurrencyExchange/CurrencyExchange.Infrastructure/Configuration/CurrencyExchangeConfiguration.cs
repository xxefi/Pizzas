using CurrencyExchange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyExchange.Infrastructure.Configuration;

public class CurrencyExchangeConfiguration : IEntityTypeConfiguration<CurrencyExchangeRateEntity>
{
    public void Configure(EntityTypeBuilder<CurrencyExchangeRateEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FromCurrency).IsRequired().HasMaxLength(3); 
        builder.Property(x => x.ToCurrency).IsRequired().HasMaxLength(3);
        builder.Property(x => x.ExchangeRate).HasColumnType("decimal(18,6)").IsRequired();
        builder.Property(x => x.LastUpdated).IsRequired();
        builder.HasIndex(x => new { x.FromCurrency, x.ToCurrency }).IsUnique();
    }
}