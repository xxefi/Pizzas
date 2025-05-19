using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class PizzaRepository : IPizzaRepository
{
    private readonly PizzasContext _context;

    public PizzaRepository(PizzasContext context)
        => _context = context;

    public async Task<PizzaEntity> GetByIdAsync(string id)
        => await _context.Pizzas
               .Include(p => p.Category)
               .Include(p => p.PizzaIngredients)
               .ThenInclude(i => i.Ingredient)
               .Include(p => p.Prices)
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "PizzaNotFound");

    public async Task<IEnumerable<PizzaEntity>> GetAllAsync()
    {
        var pizzas = await _context.Pizzas
            .Include(p => p.Category)
            .Include(p => p.PizzaIngredients)
            .ThenInclude(i => i.Ingredient)
            .Include(p => p.Prices)
            .AsNoTracking()
            .ToListAsync();

        return pizzas;
    }

    public async Task AddAsync(PizzaEntity pizzaEntity)
        => await _context.Pizzas.AddAsync(pizzaEntity);

    public async Task UpdateAsync(IEnumerable<PizzaEntity> pizzas)
    {
        foreach (var pizza in pizzas)
        {
            var result = await _context.Pizzas
                .Where(p => p.Id == pizza.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.CategoryId, pizza.CategoryId)
                    .SetProperty(p => p.Name, pizza.Name)
                    .SetProperty(p => p.Description, pizza.Description)
                    .SetProperty(p => p.ImageUrl, pizza.ImageUrl)
                    .SetProperty(p => p.Size, pizza.Size)
                    .SetProperty(p => p.Stock, pizza.Stock)
                    .SetProperty(p => p.Top, pizza.Top)
                    .SetProperty(p => p.Rating, pizza.Rating)
                    .SetProperty(p => p.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "PizzaNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Pizzas
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "PizzaNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<PizzaEntity, bool>> predicate)
        => await _context.Pizzas.AnyAsync(predicate);

    public async Task<ICollection<PizzaEntity>> FindAsync(Expression<Func<PizzaEntity, bool>> predicate)
        => await _context.Pizzas
            .Include(p => p.Category)
            .Include(p => p.PizzaIngredients)
            .ThenInclude(i => i.Ingredient)
            .Include(p => p.Prices)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
