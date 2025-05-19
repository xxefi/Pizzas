using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Commands;

public class CreatePizzaHandler : IRequestHandler<CreatePizzaCommand, PizzaDto>
{
    private readonly IPizzaService _pizzaService;

    public CreatePizzaHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<PizzaDto> Handle(CreatePizzaCommand request, CancellationToken cancellationToken)
    {
        return await _pizzaService.CreatePizzaAsync(request.Pizza);
    }
}