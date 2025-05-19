using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IBasketRepository
{
    Task<BasketEntity> GetByIdAsync(string id);
    Task<IEnumerable<BasketEntity>> GetAllAsync();
    Task AddAsync(BasketEntity basket);
    Task UpdateAsync(IEnumerable<BasketEntity> baskets);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<BasketEntity, bool>> predicate);
    Task<ICollection<BasketEntity>> FindAsync(Expression<Func<BasketEntity, bool>> predicate);
}