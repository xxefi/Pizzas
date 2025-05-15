using System.Linq.Expressions;
using Pizzas.Core.Entities.Auth;

namespace Pizzas.Core.Abstractions.Repositories.Auth;

public interface IBlackListedRepository
{
    Task<BlackListedEntity> GetByIdAsync(int id);
    Task<IEnumerable<BlackListedEntity>> GetAllAsync();
    Task AddAsync(BlackListedEntity blackListedEntity);
    Task UpdateAsync(IEnumerable<BlackListedEntity> blackLists);
    Task DeleteAsync(int id);
    Task<bool> AnyAsync(Expression<Func<BlackListedEntity, bool>> predicate);
    Task<ICollection<BlackListedEntity>> FindAsync(Expression<Func<BlackListedEntity, bool>> predicate);
}