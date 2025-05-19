using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Order;

namespace Pizzas.RequestPipeline.Handlers.Order.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderService _orderService;

    public CreateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        return _orderService.CreateOrderAsync(request.Order);
    }
}