using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Commands;

public class UpdatePizzaHandler : IRequestHandler<UpdatePizzaCommand, PizzaDto>
{
    private readonly IPizzaService _pizzaService;

    public UpdatePizzaHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<PizzaDto> Handle(UpdatePizzaCommand request, CancellationToken cancellationToken)
    {
        return await _pizzaService.UpdatePizzaAsync(request.Id, request.Pizza);
    }
}