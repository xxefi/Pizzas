using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Basket;

namespace Pizzas.RequestPipeline.Handlers.Basket.Commands;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, BasketDto>
{
    private readonly IBasketService _basketService;

    public AddItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }
    public async Task<BasketDto> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        return await _basketService.AddItemAsync(request.PizzaId, request.Quantity,
            request.TargetCurrency, request.Size);
    }
}