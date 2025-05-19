using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Pizza;

public record GetPopularPizzasQuery(string TargetCurrency) : IRequest<IEnumerable<PizzaDto>>;