using CurrencyExchange.Domain.Abstractions.Repositories;
using CurrencyExchange.Domain.Entities;
using CurrencyExchange.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Infrastructure.Repositories;

public class CurrencyExchangeRateRepository : ICurrencyExchangeRateRepository
{
    private readonly CurrencyExchangeContext _context;

    public CurrencyExchangeRateRepository(CurrencyExchangeContext context)
        => _context = context;
    
    public async Task<CurrencyExchangeRateEntity> GetAsync(string fromCurrency, string toCurrency)
    {
        return await _context.CurrencyExchangeRates
            .FirstOrDefaultAsync(r => r.FromCurrency == fromCurrency && r.ToCurrency == toCurrency);
    }

    public async Task<IEnumerable<CurrencyExchangeRateEntity>> GetAllAsync()
        => await _context.CurrencyExchangeRates.ToListAsync();

    public async Task AddAsync(CurrencyExchangeRateEntity entity)
    {
        await _context.CurrencyExchangeRates.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CurrencyExchangeRateEntity entity)
    {
        _context.CurrencyExchangeRates.Update(entity);
        await _context.SaveChangesAsync();
    }
}