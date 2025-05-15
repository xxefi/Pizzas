using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class IngredientRepository : IIngredientRepository
{
    private readonly PizzasContext _context;

    public IngredientRepository(PizzasContext context)
        => _context = context;

    public async Task<IngredientEntity> GetByIdAsync(string id)
        => await _context.Ingredients
               .AsNoTracking()
               .FirstOrDefaultAsync(i => i.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "IngredientNotFound");

    public async Task<IEnumerable<IngredientEntity>> GetAllAsync()
    {
        var ingredients = await _context.Ingredients
            .AsNoTracking()
            .ToListAsync();

        return ingredients;
    }

    public async Task AddAsync(IngredientEntity entity)
        => await _context.Ingredients.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<IngredientEntity> entities)
    {
        foreach (var entity in entities)
        {
            var result = await _context.Ingredients
                .Where(i => i.Id == entity.Id)
                .ExecuteUpdateAsync(i => i
                    .SetProperty(i => i.Name, entity.Name)
                    .SetProperty(i => i.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "IngredientNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Ingredients
            .Where(i => i.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "IngredientNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<IngredientEntity, bool>> predicate)
        => await _context.Ingredients.AnyAsync(predicate);

    public async Task<ICollection<IngredientEntity>> FindAsync(Expression<Func<IngredientEntity, bool>> predicate)
        => await _context.Ingredients
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}