using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Enums;
using Pizzas.RequestPipeline.Queries.Order;

namespace Pizzas.RequestPipeline.Handlers.Order.Queries;

public class GetOrderStatusQueryHandler : IRequestHandler<GetOrderStatusQuery, OrderStatus>
{
    private readonly IOrderService _orderService;

    public GetOrderStatusQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<OrderStatus> Handle(GetOrderStatusQuery request, CancellationToken cancellationToken)
    {
        return _orderService.GetOrderStatusAsync(request.OrderId);
    }
}