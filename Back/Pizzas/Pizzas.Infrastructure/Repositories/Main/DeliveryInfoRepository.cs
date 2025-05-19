using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class DeliveryInfoRepository : IDeliveryInfoRepository
{
    private readonly PizzasContext _context;

    public DeliveryInfoRepository(PizzasContext context)
        => _context = context;

    public async Task<DeliveryInfoEntity> GetByIdAsync(string id)
        => await _context.DeliveryInfos
               .Include(d => d.Order)
               .Include(d => d.User)
               .AsNoTracking()
               .FirstOrDefaultAsync(d => d.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "DeliveryInfoNotFound");

    public async Task<IEnumerable<DeliveryInfoEntity>> GetAllAsync()
    {
        var deliveryInfos = await _context.DeliveryInfos
            .Include(d => d.Order)
            .Include(d => d.User)
            .AsNoTracking()
            .ToListAsync();

        return deliveryInfos;
    }

    public async Task AddAsync(DeliveryInfoEntity entity)
        => await _context.DeliveryInfos.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<DeliveryInfoEntity> entities)
    {
        foreach (var entity in entities)
        {
            var result = await _context.DeliveryInfos
                .Where(d => d.Id == entity.Id)
                .ExecuteUpdateAsync(d => d
                    .SetProperty(d => d.Address, entity.Address)
                    .SetProperty(d => d.City, entity.City)
                    .SetProperty(d => d.PostalCode, entity.PostalCode)
                    .SetProperty(d => d.PhoneNumber, entity.PhoneNumber)
                    .SetProperty(d => d.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "DeliveryInfoNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.DeliveryInfos
            .Where(d => d.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "DeliveryInfoNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<DeliveryInfoEntity, bool>> predicate)
        => await _context.DeliveryInfos.AnyAsync(predicate);

    public async Task<ICollection<DeliveryInfoEntity>> FindAsync(Expression<Func<DeliveryInfoEntity, bool>> predicate)
        => await _context.DeliveryInfos
            .Include(d => d.Order)
            .Include(d => d.User)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
