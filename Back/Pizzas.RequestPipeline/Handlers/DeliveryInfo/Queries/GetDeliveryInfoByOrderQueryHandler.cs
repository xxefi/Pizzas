using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.DeliveryInfo;

namespace Pizzas.RequestPipeline.Handlers.DeliveryInfo.Queries;

public class GetDeliveryInfoByOrderQueryHandler : IRequestHandler<GetDeliveryInfoByOrderQuery, DeliveryInfoDto>
{
    private readonly IDeliveryInfoService _deliveryInfoService;

    public GetDeliveryInfoByOrderQueryHandler(IDeliveryInfoService deliveryInfoService)
    {
        _deliveryInfoService = deliveryInfoService;
    }
    public async Task<DeliveryInfoDto> Handle(GetDeliveryInfoByOrderQuery request, CancellationToken cancellationToken)
    {
        return await _deliveryInfoService.GetByOrderIdAsync(request.OrderId);
    }
}