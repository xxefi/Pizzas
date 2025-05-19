using MediatR;

namespace Pizzas.RequestPipeline.Queries.Basket;

public record GetTotalQuery(string Currency) : IRequest<decimal>;