using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Basket;

namespace Pizzas.RequestPipeline.Handlers.Basket.Queries;

public class GetItemsCountQueryHandler : IRequestHandler<GetItemsCountQuery, int>
{
    private readonly IBasketService _basketService;

    public GetItemsCountQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    public async Task<int> Handle(GetItemsCountQuery request, CancellationToken cancellationToken)
    {
        return await _basketService.GetItemsCountAsync();
    }
}