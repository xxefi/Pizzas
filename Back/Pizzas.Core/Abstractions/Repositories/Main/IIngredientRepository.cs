using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IIngredientRepository
{
    Task<IngredientEntity> GetByIdAsync(string id);
    Task<IEnumerable<IngredientEntity>> GetAllAsync();
    Task AddAsync(IngredientEntity entity);
    Task UpdateAsync(IEnumerable<IngredientEntity> entities);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<IngredientEntity, bool>> predicate);
    Task<ICollection<IngredientEntity>> FindAsync(Expression<Func<IngredientEntity, bool>> predicate);
}