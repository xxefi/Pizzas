using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Commands.Basket;

namespace Pizzas.RequestPipeline.Handlers.Basket.Commands;

public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommand, bool>
{
    private readonly IBasketService _basketService;

    public ClearBasketCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }
    public async Task<bool> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
    {
        return await _basketService.ClearBasketAsync(request.TargetCurrency);
    }
}