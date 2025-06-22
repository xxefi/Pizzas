using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Pizza;

public record GetPizzasQuery(string TargetCurrency) : IRequest<IEnumerable<PizzaDto>>;