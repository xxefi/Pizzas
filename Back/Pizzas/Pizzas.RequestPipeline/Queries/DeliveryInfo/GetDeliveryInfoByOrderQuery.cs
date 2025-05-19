using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.DeliveryInfo;

public record GetDeliveryInfoByOrderQuery(string OrderId) : IRequest<DeliveryInfoDto>;