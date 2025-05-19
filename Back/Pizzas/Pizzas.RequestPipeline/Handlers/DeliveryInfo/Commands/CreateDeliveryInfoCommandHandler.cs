using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.DeliveryInfo;

namespace Pizzas.RequestPipeline.Handlers.DeliveryInfo.Commands;

public class CreateDeliveryInfoCommandHandler : IRequestHandler<CreateDeliveryInfoCommand, DeliveryInfoDto>
{
    private readonly IDeliveryInfoService _deliveryInfoService;

    public CreateDeliveryInfoCommandHandler(IDeliveryInfoService deliveryInfoService)
    {
        _deliveryInfoService = deliveryInfoService;
    }
    
    public async Task<DeliveryInfoDto> Handle(CreateDeliveryInfoCommand request, CancellationToken cancellationToken)
    {
        return await _deliveryInfoService.CreateAsync(request.DeliveryInfo);
    }
}