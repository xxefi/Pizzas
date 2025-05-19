using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IDeliveryInfoRepository
{
    Task<DeliveryInfoEntity> GetByIdAsync(string id);
    Task<IEnumerable<DeliveryInfoEntity>> GetAllAsync();
    Task AddAsync(DeliveryInfoEntity entity);
    Task UpdateAsync(IEnumerable<DeliveryInfoEntity> entities);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<DeliveryInfoEntity, bool>> predicate);
    Task<ICollection<DeliveryInfoEntity>> FindAsync(Expression<Func<DeliveryInfoEntity, bool>> predicate);
}