using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Basket;

namespace Pizzas.RequestPipeline.Handlers.Basket.Queries;

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDto>
{
    private readonly IBasketService _basketService;

    public GetBasketQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        return await _basketService.GetBasketAsync(request.TargetCurrency);
    }
}