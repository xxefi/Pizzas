using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IPermissionRepository
{
    Task<PermissionEntity> GetByIdAsync(string id);
    Task<IEnumerable<PermissionEntity>> GetAllAsync();
    Task AddAsync(PermissionEntity entity);
    Task UpdateAsync(IEnumerable<PermissionEntity> entities);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<PermissionEntity, bool>> predicate);
    Task<ICollection<PermissionEntity>> FindAsync(Expression<Func<PermissionEntity, bool>> predicate);
}