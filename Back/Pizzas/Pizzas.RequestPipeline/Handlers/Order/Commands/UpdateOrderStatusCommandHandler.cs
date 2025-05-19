using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Order;

namespace Pizzas.RequestPipeline.Handlers.Order.Commands;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDto>
{
    private readonly IOrderService _orderService;

    public UpdateOrderStatusCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<OrderDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        return await _orderService.UpdateOrderStatusAsync(request.OrderId, request.NewStatus);
    }
}