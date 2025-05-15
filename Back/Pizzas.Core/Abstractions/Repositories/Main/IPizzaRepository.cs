using System.Linq.Expressions;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Core.Abstractions.Repositories.Main;

public interface IPizzaRepository
{
    Task<PizzaEntity> GetByIdAsync(string id);
    Task<IEnumerable<PizzaEntity>> GetAllAsync();
    Task AddAsync(PizzaEntity pizzaEntity);
    Task UpdateAsync(IEnumerable<PizzaEntity> pizzas);
    Task DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<PizzaEntity, bool>> predicate);
    Task<ICollection<PizzaEntity>> FindAsync(Expression<Func<PizzaEntity, bool>> predicate);
}