using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Dashboard;

public record GetDashboardStatisticsQuery : IRequest<DashboardStatsDto>;