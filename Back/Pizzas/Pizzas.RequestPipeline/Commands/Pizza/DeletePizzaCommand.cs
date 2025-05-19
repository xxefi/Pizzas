using MediatR;

namespace Pizzas.RequestPipeline.Commands.Pizza;

public record DeletePizzaCommand(string Id) : IRequest<bool>;