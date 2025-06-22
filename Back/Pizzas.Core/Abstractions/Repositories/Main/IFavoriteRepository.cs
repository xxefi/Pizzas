using System.Linq.Expressions;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IFavoriteRepository
{
    Task<FavoriteEntity> GetByIdAsync(string id);
    Task<IEnumerable<FavoriteEntity>> GetAllAsync();
    Task AddAsync(FavoriteEntity favoriteEntity);
    Task UpdateAsync(FavoriteEntity favoriteEntity);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<FavoriteEntity, bool>> predicate);
    Task<ICollection<FavoriteEntity>> FindAsync(Expression<Func<FavoriteEntity, bool>> predicate);
}