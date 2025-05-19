using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly PizzasContext _context;

    public OrderItemRepository(PizzasContext context)
        => _context = context;

    public async Task<OrderItemEntity> GetByIdAsync(string id)
        => await _context.OrderItems
               .Include(oi => oi.Order)
               .Include(oi => oi.Pizza)
               .AsNoTracking()
               .FirstOrDefaultAsync(oi => oi.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "OrderItemNotFound");

    public async Task<IEnumerable<OrderItemEntity>> GetAllAsync()
    {
        var orderItems = await _context.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Pizza)
            .AsNoTracking()
            .ToListAsync();

        return orderItems;
    }

    public async Task AddAsync(OrderItemEntity entity)
        => await _context.OrderItems.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<OrderItemEntity> entities)
    {
        foreach (var entity in entities)
        {
            var result = await _context.OrderItems
                .Where(oi => oi.Id == entity.Id)
                .ExecuteUpdateAsync(oi => oi
                    .SetProperty(oi => oi.Quantity, entity.Quantity)
                    .SetProperty(oi => oi.Price, entity.Price)
                    .SetProperty(oi => oi.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "OrderItemNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.OrderItems
            .Where(oi => oi.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "OrderItemNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<OrderItemEntity, bool>> predicate)
        => await _context.OrderItems.AnyAsync(predicate);

    public async Task<ICollection<OrderItemEntity>> FindAsync(Expression<Func<OrderItemEntity, bool>> predicate)
        => await _context.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Pizza)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
