using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Order;

namespace Pizzas.RequestPipeline.Handlers.Order.Commands;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, OrderDto>
{
    private readonly IOrderService _orderService;

    public CancelOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<OrderDto> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        return _orderService.CancelOrderAsync(request.OrderId);
    }
}