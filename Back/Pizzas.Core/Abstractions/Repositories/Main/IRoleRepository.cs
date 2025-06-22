using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;


public interface IRoleRepository
{
    Task<RoleEntity> GetByIdAsync(string id);
    Task<IEnumerable<RoleEntity>> GetAllAsync();
    Task AddAsync(RoleEntity roleEntity);
    Task UpdateAsync(IEnumerable<RoleEntity> roles);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<RoleEntity, bool>> predicate);
    Task<ICollection<RoleEntity>> FindAsync(Expression<Func<RoleEntity, bool>> predicate);
}