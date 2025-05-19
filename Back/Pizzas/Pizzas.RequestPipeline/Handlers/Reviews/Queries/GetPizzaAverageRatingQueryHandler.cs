using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Reviews;

namespace Pizzas.RequestPipeline.Handlers.Reviews.Queries;

public class GetPizzaAverageRatingQueryHandler : IRequestHandler<GetPizzaAverageRatingQuery, double>
{
    private readonly IReviewService _reviewService;

    public GetPizzaAverageRatingQueryHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<double> Handle(GetPizzaAverageRatingQuery request, CancellationToken cancellationToken)
    {
        return await _reviewService.GetPizzaAverageRatingAsync(request.PizzaId);
    }
}