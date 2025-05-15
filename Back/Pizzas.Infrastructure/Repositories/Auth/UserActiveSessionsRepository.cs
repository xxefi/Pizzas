using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Auth;
using Pizzas.Core.Entities.Auth;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Auth;

public class UserActiveSessionsRepository : IUserActiveSessionsRepository
{
    private readonly PizzasContext _context;

    public UserActiveSessionsRepository(PizzasContext context)
        => _context = context;
    
    public async Task<UserActiveSessionsEntity> GetByIdAsync(string id)
        => await _context.UserActiveSessions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false)
        ?? throw new PizzasException(ExceptionType.NotFound, "UserDeviceTokenNotFound"); 

    public async Task<IEnumerable<UserActiveSessionsEntity>> GetAllAsync()
    {
        var userDeviceTokens = await _context.UserActiveSessions
            .AsNoTracking()
            .ToListAsync()
            .ConfigureAwait(false);
        
        return userDeviceTokens.Any() ? userDeviceTokens : throw new PizzasException(ExceptionType.NotFound, "NoUserDeviceTokensFound");
    }

    public async Task AddAsync(UserActiveSessionsEntity userActiveSessionsEntity)
        => await _context.UserActiveSessions.AddAsync(userActiveSessionsEntity);

    public async Task UpdateAsync(IEnumerable<UserActiveSessionsEntity> userDeviceTokens)
    {
        foreach (var userDeviceToken in userDeviceTokens)
        {
            var updatedCount = await _context.UserActiveSessions
                .Where(u => u.Id == userDeviceToken.Id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.UserId, userDeviceToken.UserId)
                    .SetProperty(u => u.AccessToken, userDeviceToken.AccessToken)
                    .SetProperty(u => u.RefreshToken, userDeviceToken.RefreshToken)
                    .SetProperty(u => u.RefreshTokenExpiryTime, userDeviceToken.RefreshTokenExpiryTime)
                    .SetProperty(u => u.DeviceInfo, userDeviceToken.DeviceInfo));
            
            if (updatedCount == 0)  throw new PizzasException(ExceptionType.NotFound, "UserDeviceTokenNotFound"); 
        }
    }

    public async Task DeleteAsync(string id)
    {
        var item = await _context.UserActiveSessions
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        
        if (item == 0) throw new PizzasException(ExceptionType.NotFound, "UserDeviceTokenNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<UserActiveSessionsEntity, bool>> predicate)
        => await _context.UserActiveSessions.AnyAsync(predicate);

    public async Task<ICollection<UserActiveSessionsEntity>> FindAsync(Expression<Func<UserActiveSessionsEntity, bool>> predicate)
        => await _context.UserActiveSessions
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}