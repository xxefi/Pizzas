using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IOrderItemRepository
{
    Task<OrderItemEntity> GetByIdAsync(string id);
    Task<IEnumerable<OrderItemEntity>> GetAllAsync();
    Task AddAsync(OrderItemEntity entity);
    Task UpdateAsync(IEnumerable<OrderItemEntity> entities);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<OrderItemEntity, bool>> predicate);
    Task<ICollection<OrderItemEntity>> FindAsync(Expression<Func<OrderItemEntity, bool>> predicate);
}