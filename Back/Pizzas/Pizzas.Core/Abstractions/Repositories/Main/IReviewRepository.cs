using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IReviewRepository
{
    Task<ReviewEntity> GetByIdAsync(string id);
    Task<IEnumerable<ReviewEntity>> GetAllAsync();
    Task AddAsync(ReviewEntity reviewEntity);
    Task UpdateAsync(IEnumerable<ReviewEntity> reviewEntities);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<ReviewEntity, bool>> predicate);
    Task<ICollection<ReviewEntity>> FindAsync(Expression<Func<ReviewEntity, bool>> predicate);
}