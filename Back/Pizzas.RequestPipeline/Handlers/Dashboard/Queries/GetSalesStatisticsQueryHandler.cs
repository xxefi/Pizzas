using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Dashboard;

namespace Pizzas.RequestPipeline.Handlers.Dashboard.Queries;

public class GetSalesStatisticsQueryHandler : IRequestHandler<GetSalesStatisticsQuery, SalesStatsDto>
{
    private readonly IDashboardService _dashboardService;

    public GetSalesStatisticsQueryHandler(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<SalesStatsDto> Handle(GetSalesStatisticsQuery request, CancellationToken cancellationToken)
    {
        return await _dashboardService.GetSalesStatisticsAsync(request.StartDate, request.EndDate);
    }
}