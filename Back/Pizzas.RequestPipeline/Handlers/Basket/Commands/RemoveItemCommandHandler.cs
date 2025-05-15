using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Basket;

namespace Pizzas.RequestPipeline.Handlers.Basket.Commands;

public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, BasketDto>
{
    private readonly IBasketService _basketService;

    public RemoveItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }
    public async Task<BasketDto> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
    {
        return await _basketService.RemoveItemAsync(request.BasketItemId, request.TargetCurrency);
    }
}