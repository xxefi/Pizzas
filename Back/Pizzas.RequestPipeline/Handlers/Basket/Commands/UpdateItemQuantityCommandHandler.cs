using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Basket;

namespace Pizzas.RequestPipeline.Handlers.Basket.Commands;

public class UpdateItemQuantityCommandHandler : IRequestHandler<UpdateItemQuantityCommand, BasketDto>
{
    private readonly IBasketService _basketService;

    public UpdateItemQuantityCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }
    public async Task<BasketDto> Handle(UpdateItemQuantityCommand request, CancellationToken cancellationToken)
    {
        return await _basketService.UpdateItemQuantityAsync(request.BasketItemId, request.Quantity, request.TargetCurrency);
    }
}