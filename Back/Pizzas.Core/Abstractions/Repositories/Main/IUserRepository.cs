using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IUserRepository
{
    Task<UserEntity> GetByIdAsync(string id);
    Task<IEnumerable<UserEntity>> GetAllAsync();
    Task AddAsync(UserEntity userEntity);
    Task UpdateAsync(IEnumerable<UserEntity> users);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<UserEntity, bool>> predicate);
    Task<ICollection<UserEntity>> FindAsync(Expression<Func<UserEntity, bool>> predicate);
}