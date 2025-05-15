using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;

namespace Pizzas.Infrastructure.Repositories.Main;

public class ReviewRepository : IReviewRepository
{
    private readonly PizzasContext _context;

    public ReviewRepository(PizzasContext context)
        => _context = context;
    
    public async Task<ReviewEntity> GetByIdAsync(string id)
        => await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Pizza)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id)
        ?? throw new PizzasException(ExceptionType.NotFound, "ReviewNotFound");

    public async Task<IEnumerable<ReviewEntity>> GetAllAsync()
    {
        var reviews = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Pizza)
            .AsNoTracking()
            .ToListAsync();

        return reviews;
    }

    public async Task AddAsync(ReviewEntity reviewEntity)
        => await _context.Reviews.AddAsync(reviewEntity);

    public async Task UpdateAsync(IEnumerable<ReviewEntity> reviewEntities)
    {
        foreach (var reviewEntity in reviewEntities)
        {
            var result = await _context.Reviews
                .Where(r => r.Id == reviewEntity.Id)
                .ExecuteUpdateAsync(r => r
                    .SetProperty(r => r.Content, reviewEntity.Content)
                    .SetProperty(r => r.Rating, reviewEntity.Rating)
                    .SetProperty(r => r.UpdatedAt, DateTime.UtcNow));

            if (result == 0)
                throw new PizzasException(ExceptionType.NotFound, "ReviewNotFound");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _context.Reviews
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();

        if (result == 0)
            throw new PizzasException(ExceptionType.NotFound, "ReviewNotFound");
    }

    public async Task<bool> AnyAsync(Expression<Func<ReviewEntity, bool>> predicate)
        => await _context.Reviews.AnyAsync(predicate);

    public async Task<ICollection<ReviewEntity>> FindAsync(Expression<Func<ReviewEntity, bool>> predicate)
        => await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Pizza)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
}