using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IReviewService
{
    Task<ReviewDto> CreateReviewAsync(CreateReviewDto createDto);
    Task<ReviewDto> UpdateReviewAsync(string id, UpdateReviewDto updateDto);
    Task<bool> DeleteReviewAsync(string reviewId);
    Task<IEnumerable<ReviewDto>> GetPizzaReviewsAsync(string pizzaId);
    Task<double> GetPizzaAverageRatingAsync(string pizzaId);
    Task<bool> UpdatePizzaRatingAsync(string pizzaId);
}