using System.Linq.Expressions;
using Pizzas.Core.Entities.Auth;

namespace Pizzas.Core.Abstractions.Repositories.Auth;

public interface IUserActiveSessionsRepository
{
    Task<UserActiveSessionsEntity> GetByIdAsync(string id);
    Task<IEnumerable<UserActiveSessionsEntity>> GetAllAsync();
    Task AddAsync(UserActiveSessionsEntity userActiveSessionsEntity);
    Task UpdateAsync(IEnumerable<UserActiveSessionsEntity> userDeviceTokens);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<UserActiveSessionsEntity, bool>> predicate);
    Task<ICollection<UserActiveSessionsEntity>> FindAsync(Expression<Func<UserActiveSessionsEntity, bool>> predicate);
}