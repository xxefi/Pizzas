using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class BasketRepository : IBasketRepository
{
    private readonly PizzasContext _context;

    public BasketRepository(PizzasContext context)
        => _context = context;

    public async Task<BasketEntity> GetByIdAsync(string id)
        => await _context.Baskets
               .Include(b => b.Items)
                   .ThenInclude(i => i.Pizza)
               .Include(b => b.User)
               .AsNoTracking()
               .FirstOrDefaultAsync(b => b.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "BasketNotFound");

    public async Task<IEnumerable<BasketEntity>> GetAllAsync()
    {
        var baskets = await _context.Baskets
            .Include(b => b.Items)
                .ThenInclude(i => i.Pizza)
            .Include(b => b.User)
            .AsNoTracking()
            .ToListAsync();

        return baskets;
    }

    public async Task AddAsync(BasketEntity basket)
        => await _context.Baskets.AddAsync(basket);

    public async Task UpdateAsync(IEnumerable<BasketEntity> baskets)
    {
        foreach (var basket in baskets)
        {
            var result = await _context.Baskets
                .Where(b => b.Id == basket.Id)
                .ExecuteUpdateAsync(b => b
                    .SetProperty(b => b.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "BasketNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Baskets
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "BasketNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<BasketEntity, bool>> predicate)
        => await _context.Baskets.AnyAsync(predicate);

    public async Task<ICollection<BasketEntity>> FindAsync(Expression<Func<BasketEntity, bool>> predicate)
        => await _context.Baskets
            .Include(b => b.Items)
                .ThenInclude(i => i.Pizza)
            .Include(b => b.User)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
