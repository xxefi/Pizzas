using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class BasketItemRepository : IBasketItemRepository
{
    private readonly PizzasContext _context;

    public BasketItemRepository(PizzasContext context)
        => _context = context;

    public async Task<BasketItemEntity> GetByIdAsync(string id)
        => await _context.BasketItems
               .Include(i => i.Pizza)
               .AsNoTracking()
               .FirstOrDefaultAsync(i => i.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "BasketItemNotFound");

    public async Task<IEnumerable<BasketItemEntity>> GetAllAsync()
    {
        var items = await _context.BasketItems
            .Include(i => i.Pizza)
            .AsNoTracking()
            .ToListAsync();

        return items;
    }

    public async Task AddAsync(BasketItemEntity item)
        => await _context.BasketItems.AddAsync(item);

    public async Task UpdateAsync(IEnumerable<BasketItemEntity> items)
    {
        foreach (var item in items)
        {
            var result = await _context.BasketItems
                .Where(i => i.Id == item.Id)
                .ExecuteUpdateAsync(i => i
                    .SetProperty(i => i.Quantity, item.Quantity)
                    .SetProperty(i => i.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "BasketItemNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.BasketItems
            .Where(i => i.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "BasketItemNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<BasketItemEntity, bool>> predicate)
        => await _context.BasketItems.AnyAsync(predicate);

    public async Task<ICollection<BasketItemEntity>> FindAsync(Expression<Func<BasketItemEntity, bool>> predicate)
        => await _context.BasketItems
            .Include(i => i.Pizza)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
