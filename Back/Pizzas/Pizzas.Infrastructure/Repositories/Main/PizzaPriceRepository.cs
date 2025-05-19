using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class PizzaPriceRepository : IPizzaPriceRepository
{
    private readonly PizzasContext _context;

    public PizzaPriceRepository(PizzasContext context)
        => _context = context;

    public async Task<PizzaPriceEntity?> GetByIdAsync(string id)
        => await _context.PizzaPrices
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "PizzaPriceNotFound");

    public async Task<IEnumerable<PizzaPriceEntity>> GetAllAsync()
    {
        var prices = await _context.PizzaPrices
            .AsNoTracking()
            .ToListAsync();

        return prices;
    }

    public async Task AddAsync(PizzaPriceEntity price)
        => await _context.PizzaPrices.AddAsync(price);

    public async Task UpdateAsync(IEnumerable<PizzaPriceEntity> prices)
    {
        foreach (var price in prices)
        {
            var result = await _context.PizzaPrices
                .Where(p => p.Id == price.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.OriginalPrice, price.OriginalPrice)
                    .SetProperty(p => p.DiscountPrice, price.DiscountPrice)
                    .SetProperty(p => p.Size, price.Size));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "PizzaPriceNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.PizzaPrices
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "PizzaPriceNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<PizzaPriceEntity, bool>> predicate)
        => await _context.PizzaPrices.AnyAsync(predicate);

    public async Task<ICollection<PizzaPriceEntity>> FindAsync(Expression<Func<PizzaPriceEntity, bool>> predicate)
        => await _context.PizzaPrices
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}