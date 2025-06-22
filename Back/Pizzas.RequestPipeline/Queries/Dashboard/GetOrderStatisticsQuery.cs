using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Dashboard;

public record GetOrderStatisticsQuery(DateTime Date) : IRequest<OrderStatsDto>;