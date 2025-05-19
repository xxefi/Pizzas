using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IOrderRepository
{
    Task<OrderEntity> GetByIdAsync(string id);
    Task<IEnumerable<OrderEntity>> GetAllAsync();
    Task AddAsync(OrderEntity entity);
    Task UpdateAsync(IEnumerable<OrderEntity> entities);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<OrderEntity, bool>> predicate);
    Task<ICollection<OrderEntity>> FindAsync(Expression<Func<OrderEntity, bool>> predicate);
}