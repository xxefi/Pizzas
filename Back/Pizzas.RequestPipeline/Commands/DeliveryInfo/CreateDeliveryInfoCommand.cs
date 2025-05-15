using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.DeliveryInfo;

public record CreateDeliveryInfoCommand(CreateDeliveryInfoDto DeliveryInfo) : IRequest<DeliveryInfoDto>;