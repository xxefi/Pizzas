using System.Linq.Expressions;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface ICategoryRepository
{
    Task<CategoryEntity> GetByIdAsync(string id);
    Task<IEnumerable<CategoryEntity>> GetAllAsync();
    Task AddAsync(CategoryEntity categoryEntity);
    Task UpdateAsync(IEnumerable<CategoryEntity> categories);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<CategoryEntity, bool>> predicate);
    Task<ICollection<CategoryEntity>> FindAsync(Expression<Func<CategoryEntity, bool>> predicate);
} 