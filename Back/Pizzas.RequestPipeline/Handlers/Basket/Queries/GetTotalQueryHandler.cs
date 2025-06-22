using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Basket;

namespace Pizzas.RequestPipeline.Handlers.Basket.Queries;

public class GetTotalQueryHandler : IRequestHandler<GetTotalQuery, decimal>
{
    private readonly IBasketService _basketService;

    public GetTotalQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    public async Task<decimal> Handle(GetTotalQuery request, CancellationToken cancellationToken)
    {
        return await _basketService.GetTotalAsync(request.Currency);
    }
}