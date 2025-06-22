using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Order;

namespace Pizzas.RequestPipeline.Handlers.Order.Queries;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
{
    private readonly IOrderService _orderService;

    public GetOrderQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        return await _orderService.GetOrderByIdAsync(request.OrderId);
    }
}