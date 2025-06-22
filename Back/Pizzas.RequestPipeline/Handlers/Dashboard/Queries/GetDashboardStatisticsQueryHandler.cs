using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Dashboard;

namespace Pizzas.RequestPipeline.Handlers.Dashboard.Queries;

public class GetDashboardStatisticsQueryHandler : IRequestHandler<GetDashboardStatisticsQuery, DashboardStatsDto>
{
    private readonly IDashboardService _dashboardService;

    public GetDashboardStatisticsQueryHandler(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<DashboardStatsDto> Handle(GetDashboardStatisticsQuery request, CancellationToken cancellationToken)
    {
        return await _dashboardService.GetDashboardStatisticsAsync();
    }
}