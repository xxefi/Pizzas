using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.PizzaPrices;

namespace Pizzas.RequestPipeline.Handlers.PizzaPrices.Commands;

public class AddPizzaPriceHandler : IRequestHandler<AddPizzaPriceCommand, PizzaPriceDto>
{
    private readonly IPizzaPriceService _pizzaPriceService;

    public AddPizzaPriceHandler(IPizzaPriceService pizzaPriceService)
    {
        _pizzaPriceService = pizzaPriceService;
    }
    public async Task<PizzaPriceDto> Handle(AddPizzaPriceCommand request, CancellationToken cancellationToken)
    {
        return await _pizzaPriceService.AddPriceAsync(request.PizzaPrice);
    }
}