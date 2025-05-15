using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IBasketItemRepository
{
    Task<BasketItemEntity> GetByIdAsync(string id);
    Task<IEnumerable<BasketItemEntity>> GetAllAsync();
    Task AddAsync(BasketItemEntity item);
    Task UpdateAsync(IEnumerable<BasketItemEntity> items);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<BasketItemEntity, bool>> predicate);
    Task<ICollection<BasketItemEntity>> FindAsync(Expression<Func<BasketItemEntity, bool>> predicate);
}