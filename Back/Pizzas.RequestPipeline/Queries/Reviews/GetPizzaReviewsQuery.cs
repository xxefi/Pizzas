using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Reviews;

public record GetPizzaReviewsQuery(string PizzaId) : IRequest<IEnumerable<ReviewDto>>;