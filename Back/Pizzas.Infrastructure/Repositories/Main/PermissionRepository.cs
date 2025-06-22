using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class PermissionRepository : IPermissionRepository
{
    private readonly PizzasContext _context;

    public PermissionRepository(PizzasContext context)
        => _context = context;

    public async Task<PermissionEntity> GetByIdAsync(string id)
        => await _context.Permissions
               .Include(p => p.Roles)
               .FirstOrDefaultAsync(p => p.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "PermissionNotFound");

    public async Task<IEnumerable<PermissionEntity>> GetAllAsync()
    {
        var permissions = await _context.Permissions
            .Include(p => p.Roles)
            .ToListAsync();

        return permissions;
    }

    public async Task AddAsync(PermissionEntity entity)
        => await _context.Permissions.AddAsync(entity);

    public async Task UpdateAsync(IEnumerable<PermissionEntity> entities)
    {
        foreach (var entity in entities)
        {
            var result = await _context.Permissions
                .Where(p => p.Id == entity.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "PermissionNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Permissions
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "PermissionNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<PermissionEntity, bool>> predicate)
        => await _context.Permissions.AnyAsync(predicate);

    public async Task<ICollection<PermissionEntity>> FindAsync(Expression<Func<PermissionEntity, bool>> predicate)
        => await _context.Permissions
            .Include(p => p.Roles)
            .Where(predicate)
            .ToListAsync();
}
