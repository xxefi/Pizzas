using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Pizza;

public record CreatePizzaCommand(CreatePizzaDto Pizza) : IRequest<PizzaDto>;