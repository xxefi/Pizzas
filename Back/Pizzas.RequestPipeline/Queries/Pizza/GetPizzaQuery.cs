using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Pizza;

public record GetPizzaQuery(string Id, string TargetCurrency) : IRequest<PizzaDto>;