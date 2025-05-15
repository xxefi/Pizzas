using System.Linq.Expressions;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IPizzaPriceRepository
{
    Task<PizzaPriceEntity?> GetByIdAsync(string id);
    Task<IEnumerable<PizzaPriceEntity>> GetAllAsync();
    Task AddAsync(PizzaPriceEntity price);
    Task UpdateAsync(IEnumerable<PizzaPriceEntity> prices);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<PizzaPriceEntity, bool>> predicate);
    Task<ICollection<PizzaPriceEntity>> FindAsync(Expression<Func<PizzaPriceEntity, bool>> predicate);
}