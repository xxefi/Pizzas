using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.DeliveryInfo;

namespace Pizzas.RequestPipeline.Handlers.DeliveryInfo.Commands;

public class UpdateDeliveryInfoCommandHandler : IRequestHandler<UpdateDeliveryInfoCommand, DeliveryInfoDto>
{
    private readonly IDeliveryInfoService _deliveryInfoService;

    public UpdateDeliveryInfoCommandHandler(IDeliveryInfoService deliveryInfoService)
    {
        _deliveryInfoService = deliveryInfoService;
    }
    public async Task<DeliveryInfoDto> Handle(UpdateDeliveryInfoCommand request, CancellationToken cancellationToken)
    {
        return await _deliveryInfoService.UpdateAsync(request.DeliveryInfo);
    }
}