using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class RoleRepository : IRoleRepository
{
    private readonly PizzasContext _context;

    public RoleRepository(PizzasContext context)
        => _context = context;

    public async Task<RoleEntity> GetByIdAsync(string id)
        => await _context.Roles
               .Include(r => r.Users)
               .Include(r => r.Permissions)
               .FirstOrDefaultAsync(r => r.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "RoleNotFound");

    public async Task<IEnumerable<RoleEntity>> GetAllAsync()
    {
        var roles = await _context.Roles
            .Include(r => r.Users)
            .Include(r => r.Permissions)
            .ToListAsync();

        return roles;
    }

    public async Task AddAsync(RoleEntity roleEntity)
        => await _context.Roles.AddAsync(roleEntity);

    public async Task UpdateAsync(IEnumerable<RoleEntity> roles)
    {
        foreach (var role in roles)
        {
            var result = await _context.Roles
                .Where(r => r.Id == role.Id)
                .ExecuteUpdateAsync(r => r
                    .SetProperty(r => r.Name, role.Name)
                    .SetProperty(r => r.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "RoleNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Roles
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "RoleNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<RoleEntity, bool>> predicate)
        => await _context.Roles.AnyAsync(predicate);

    public async Task<ICollection<RoleEntity>> FindAsync(Expression<Func<RoleEntity, bool>> predicate)
        => await _context.Roles
            .Include(r => r.Users)
            .Include(r => r.Permissions)
            .Where(predicate)
            .ToListAsync();
}
