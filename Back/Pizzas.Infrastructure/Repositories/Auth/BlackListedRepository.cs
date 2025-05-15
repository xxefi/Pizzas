using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Auth;
using Pizzas.Core.Entities.Auth;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Auth;

public class BlackListedRepository : IBlackListedRepository
{
    private readonly PizzasContext _context;

    public BlackListedRepository(PizzasContext context)
        => _context = context;
    
    public async Task<BlackListedEntity> GetByIdAsync(int id)
        => await _context.BlackListeds
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id)
            .ConfigureAwait(false)
        ?? throw new PizzasException(ExceptionType.NotFound, "BlackListItemNotFound");

    public async Task<IEnumerable<BlackListedEntity>> GetAllAsync()
    {
        var blackList = await _context.BlackListeds
            .AsNoTracking()
            .ToListAsync()
            .ConfigureAwait(false);
        
        return blackList.Any() ? blackList : throw new PizzasException(ExceptionType.NotFound, "NoBlackListItemsFound");
    }

    public async Task AddAsync(BlackListedEntity blackListedEntity)
        => await _context.BlackListeds.AddAsync(blackListedEntity);

    public async Task UpdateAsync(IEnumerable<BlackListedEntity> blackLists)
    {
        foreach (var blackListed in blackLists)
        {
            var updatedCount = await _context.BlackListeds
                .Where(b => b.Id == blackListed.Id)
                .ExecuteUpdateAsync(b => b
                    .SetProperty(b => b.AccessToken, blackListed.AccessToken)
                    .SetProperty(b => b.RefreshToken, blackListed.RefreshToken)
                    .SetProperty(b => b.IpAddress, blackListed.IpAddress)
                    .SetProperty(b => b.DeviceInfo, blackListed.DeviceInfo));
            
            if (updatedCount == 0) throw new PizzasException(ExceptionType.NotFound, "BlackListItemNotFound");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.BlackListeds
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
        
        if (item == 0) throw new PizzasException(ExceptionType.NotFound, "BlackListItemNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<BlackListedEntity, bool>> predicate)
        => await _context.BlackListeds.AnyAsync(predicate);

    public async Task<ICollection<BlackListedEntity>> FindAsync(Expression<Func<BlackListedEntity, bool>> predicate)
        => await _context.BlackListeds
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    
}