using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.Pizza;

public record UpdatePizzaCommand(string Id, UpdatePizzaDto Pizza) : IRequest<PizzaDto>;
