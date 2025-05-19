using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.PizzaPrices;

namespace Pizzas.RequestPipeline.Handlers.PizzaPrices.Commands;

public class UpdatePizzaPricesHandler : IRequestHandler<UpdatePizzaPricesCommand, PizzaPriceDto>
{
    private readonly IPizzaPriceService _pizzaPriceService;

    public UpdatePizzaPricesHandler(IPizzaPriceService pizzaPriceService)
    {
        _pizzaPriceService = pizzaPriceService;
    }
    public async Task<PizzaPriceDto> Handle(UpdatePizzaPricesCommand request, CancellationToken cancellationToken)
    {
        return await _pizzaPriceService.UpdatePricesAsync(request.PizzaId, request.Prices);
    }
}