using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Commands.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Commands;

public class DeletePizzaHandler : IRequestHandler<DeletePizzaCommand, bool>
{
    private readonly IPizzaService _pizzaService;

    public DeletePizzaHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<bool> Handle(DeletePizzaCommand request, CancellationToken cancellationToken)
    {
        return await _pizzaService.DeletePizzaAsync(request.Id);
    }
}