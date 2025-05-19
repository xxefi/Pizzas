using CurrencyExchange.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Infrastructure.Context;

public class CurrencyExchangeContext : DbContext
{
    public CurrencyExchangeContext(DbContextOptions<CurrencyExchangeContext> options) : base(options)
    {
    }
    
    public DbSet<CurrencyExchangeRateEntity> CurrencyExchangeRates { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(CurrencyExchangeContext).Assembly);
}