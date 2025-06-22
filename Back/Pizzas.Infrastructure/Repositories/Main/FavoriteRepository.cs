using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly PizzasContext _context;

    public FavoriteRepository(PizzasContext context)
        => _context = context;
    
    public async Task<FavoriteEntity> GetByIdAsync(string id)
        => await _context.Favorites
               .Include(f => f.Pizza)
                    .ThenInclude(p => p.Prices)
               .Include(f => f.Pizza)
                    .ThenInclude(p => p.Category)
               .Include(f => f.Pizza)
                    .ThenInclude(p => p.PizzaIngredients)
                        .ThenInclude(pi => pi.Ingredient)
               .Include(f => f.User)
               .AsNoTracking()
               .FirstOrDefaultAsync(f => f.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "FavoriteNotFound");

    public async Task<IEnumerable<FavoriteEntity>> GetAllAsync()
    {
        var favorites = await _context.Favorites
            .Include(f => f.Pizza)
                .ThenInclude(p => p.Prices)
            .Include(f => f.Pizza)
                .ThenInclude(p => p.Category)
            .Include(f => f.Pizza)
                .ThenInclude(p => p.PizzaIngredients)
                    .ThenInclude(pi => pi.Ingredient)
            .Include(f => f.User)
            .AsNoTracking()
            .ToListAsync();

        return favorites;
    }

    public async Task AddAsync(FavoriteEntity favoriteEntity)
        => await _context.Favorites.AddAsync(favoriteEntity);

    public async Task UpdateAsync(FavoriteEntity favoriteEntity)
    {
        var existingFavorite = await _context.Favorites
            .Where(f => f.Id == favoriteEntity.Id)
            .ExecuteUpdateAsync(f => f
                .SetProperty(f => f.PizzaId, favoriteEntity.PizzaId)
                .SetProperty(f => f.UserId, favoriteEntity.UserId)
                .SetProperty(f => f.IsActive, favoriteEntity.IsActive)
                .SetProperty(f => f.RemovedAt, DateTime.UtcNow));

        if (existingFavorite == 0) throw new PizzasException(ExceptionType.NotFound, "FavoriteNotFound");
    }

    public async Task DeleteAsync(string id)
    {
        var favorite = await _context.Favorites
            .Where(f => f.Id == id)
            .ExecuteDeleteAsync();

        if (favorite == 0) throw new PizzasException(ExceptionType.NotFound, "FavoriteNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<FavoriteEntity, bool>> predicate)
        => await _context.Favorites.AnyAsync(predicate);

    public async Task<ICollection<FavoriteEntity>> FindAsync(Expression<Func<FavoriteEntity, bool>> predicate)
        => await _context.Favorites
            .Include(f => f.Pizza)
                .ThenInclude(p => p.Prices)
            .Include(f => f.Pizza)
                .ThenInclude(p => p.Category)
            .Include(f => f.Pizza)
                .ThenInclude(p => p.PizzaIngredients)
                    .ThenInclude(pi => pi.Ingredient)
            .Include(f => f.User)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}