using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class AddressRepository : IAddressRepository
{
    private readonly PizzasContext _context;

    public AddressRepository(PizzasContext context)
        => _context = context;

    public async Task<AddressEntity> GetByIdAsync(string id)
        => await _context.Addresses
               .Include(a => a.User)
               .AsNoTracking()
               .FirstOrDefaultAsync(a => a.Id == id)
           ?? throw new PizzasException(ExceptionType.NotFound, "AddressNotFound");

    public async Task<IEnumerable<AddressEntity>> GetAllAsync()
    {
        var addresses = await _context.Addresses
            .Include(a => a.User)
            .AsNoTracking()
            .ToListAsync();

        return addresses;
    }

    public async Task AddAsync(AddressEntity addressEntity)
        => await _context.Addresses.AddAsync(addressEntity);

    public async Task UpdateAsync(IEnumerable<AddressEntity> addresses)
    {
        foreach (var address in addresses)
        {
            var result = await _context.Addresses
                .Where(a => a.Id == address.Id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(a => a.Street, address.Street)
                    .SetProperty(a => a.City, address.City)
                    .SetProperty(a => a.State, address.State)
                    .SetProperty(a => a.Country, address.Country)
                    .SetProperty(a => a.PostalCode, address.PostalCode)
                    .SetProperty(a => a.IsDefault, address.IsDefault)
                    .SetProperty(a => a.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "AddressNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Addresses
            .Include(a => a.User)
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "AddressNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<AddressEntity, bool>> predicate)
        => await _context.Addresses.AnyAsync(predicate);

    public async Task<ICollection<AddressEntity>> FindAsync(Expression<Func<AddressEntity, bool>> predicate)
        => await _context.Addresses
            .Include(a => a.User)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
} 