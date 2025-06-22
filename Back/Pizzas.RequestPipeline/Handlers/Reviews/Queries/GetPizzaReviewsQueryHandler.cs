using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Reviews;

namespace Pizzas.RequestPipeline.Handlers.Reviews.Queries;

public class GetPizzaReviewsQueryHandler : IRequestHandler<GetPizzaReviewsQuery, IEnumerable<ReviewDto>>
{
    private readonly IReviewService _reviewService;

    public GetPizzaReviewsQueryHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<IEnumerable<ReviewDto>> Handle(GetPizzaReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _reviewService.GetPizzaReviewsAsync(request.PizzaId);
    }
}