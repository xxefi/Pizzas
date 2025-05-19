using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Order;

namespace Pizzas.RequestPipeline.Handlers.Order.Queries;

public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderService _orderService;

    public GetUserOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<IEnumerable<OrderDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        return _orderService.GetUserOrdersAsync();
    }
}