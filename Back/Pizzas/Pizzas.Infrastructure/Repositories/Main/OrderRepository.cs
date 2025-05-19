using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class OrderRepository : IOrderRepository
{
    private readonly PizzasContext _context;

    public OrderRepository(PizzasContext context)
        => _context = context;

    public async Task<OrderEntity> GetByIdAsync(string id)
        => await _context.Orders
               .Include(o => o.User)
               .Include(o => o.Items)
               .ThenInclude(i => i.Pizza)
               .Include(o => o.DeliveryInfo)
               .AsNoTracking()
               .FirstOrDefaultAsync(o => o.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "OrderNotFound");
    

    public async Task<IEnumerable<OrderEntity>> GetAllAsync()
    {
        var orders = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Pizza)
            .Include(o => o.DeliveryInfo)
            .AsNoTracking()
            .ToListAsync();

        return orders;
    }

    public async Task AddAsync(OrderEntity entity)
        => await _context.Orders.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<OrderEntity> entities)
    {
        foreach (var entity in entities)
        {
            var result = await _context.Orders
                .Where(o => o.Id == entity.Id)
                .ExecuteUpdateAsync(o => o
                    .SetProperty(o => o.Status, entity.Status)
                    .SetProperty(o => o.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "OrderNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Orders
            .Where(o => o.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "OrderNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<OrderEntity, bool>> predicate)
        => await _context.Orders.AnyAsync(predicate);

    public async Task<ICollection<OrderEntity>> FindAsync(Expression<Func<OrderEntity, bool>> predicate)
        => await _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Pizza)
            .Include(o => o.DeliveryInfo)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
