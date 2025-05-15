using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class CategoryRepository : ICategoryRepository
{
    private readonly PizzasContext _context;

    public CategoryRepository(PizzasContext context)
        => _context = context;

    public async Task<CategoryEntity> GetByIdAsync(string id)
        => await _context.Categories
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "CategoryNotFound");

    public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .ToListAsync();

        return categories;
    }

    public async Task AddAsync(CategoryEntity categoryEntity)
        => await _context.Categories.AddAsync(categoryEntity);

    public async Task UpdateAsync(IEnumerable<CategoryEntity> categories)
    {
        foreach (var category in categories)
        {
            var result = await _context.Categories
                .Where(c => c.Id == category.Id)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(c => c.Name, category.Name)
                    .SetProperty(c => c.Icon, category.Icon)
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "CategoryNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Categories
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "CategoryNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<CategoryEntity, bool>> predicate)
        => await _context.Categories.AnyAsync(predicate);

    public async Task<ICollection<CategoryEntity>> FindAsync(Expression<Func<CategoryEntity, bool>> predicate)
        => await _context.Categories
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
} 