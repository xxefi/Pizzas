using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Pizza;

public record GetPizzasByIngredientsQuery(IEnumerable<string> Ingredients, string Currency) 
    : IRequest<IEnumerable<PizzaDto>>;