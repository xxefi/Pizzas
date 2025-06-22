using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Order;

namespace Pizzas.RequestPipeline.Handlers.Order.Queries;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderService _orderService;

    public GetOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderService.GetUserOrdersAsync();
    }
}