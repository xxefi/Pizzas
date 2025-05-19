using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class UserRepository : IUserRepository
{
    private readonly PizzasContext _context;

    public UserRepository(PizzasContext context)
        => _context = context;

    public async Task<UserEntity> GetByIdAsync(string id)
        => await _context.Users
               .Include(u => u.Role)
               .Include(u => u.Reviews)
               .Include(u => u.Orders)
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "UserNotFound");

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var users = await _context.Users
            .Include(u => u.Role)
            .Include(u => u.Reviews)
            .Include(u => u.Orders)
            .AsNoTracking()
            .ToListAsync();

        return users;
    }

    public async Task AddAsync(UserEntity userEntity)
        => await _context.Users.AddAsync(userEntity);

    public async Task UpdateAsync(IEnumerable<UserEntity> users)
    {
        foreach (var user in users)
        {
            var result = await _context.Users
                .Where(u => u.Id == user.Id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.Username, user.Username)
                    .SetProperty(u => u.FirstName, user.FirstName)
                    .SetProperty(u => u.LastName, user.LastName)
                    .SetProperty(u => u.Email, user.Email)
                    .SetProperty(u => u.Password, user.Password)
                    .SetProperty(u => u.Verified, user.Verified)
                    .SetProperty(u => u.RoleId, user.RoleId)
                    .SetProperty(u => u.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "UserNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "UserNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<UserEntity, bool>> predicate)
        => await _context.Users.AnyAsync(predicate);

    public async Task<ICollection<UserEntity>> FindAsync(Expression<Func<UserEntity, bool>> predicate)
        => await _context.Users
            .Include(u => u.Role)
            .Include(u => u.Reviews)
            .Include(u => u.Orders)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}
