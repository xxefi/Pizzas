using MediatR;

namespace Pizzas.RequestPipeline.Queries.Reviews;

public record GetPizzaAverageRatingQuery(string PizzaId) : IRequest<double>;