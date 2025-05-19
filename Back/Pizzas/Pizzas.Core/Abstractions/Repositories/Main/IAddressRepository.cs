using System.Linq.Expressions;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IAddressRepository
{
    Task<AddressEntity> GetByIdAsync(string id);
    Task<IEnumerable<AddressEntity>> GetAllAsync();
    Task AddAsync(AddressEntity addressEntity);
    Task UpdateAsync(IEnumerable<AddressEntity> addresses);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<AddressEntity, bool>> predicate);
    Task<ICollection<AddressEntity>> FindAsync(Expression<Func<AddressEntity, bool>> predicate);
} 