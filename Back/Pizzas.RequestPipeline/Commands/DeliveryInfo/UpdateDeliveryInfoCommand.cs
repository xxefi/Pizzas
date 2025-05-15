using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.DeliveryInfo;

public record UpdateDeliveryInfoCommand(UpdateDeliveryInfoDto DeliveryInfo) : IRequest<DeliveryInfoDto>;