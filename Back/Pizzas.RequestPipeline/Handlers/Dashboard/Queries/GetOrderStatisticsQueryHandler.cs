using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Dashboard;

namespace Pizzas.RequestPipeline.Handlers.Dashboard.Queries;

public class GetOrderStatisticsQueryHandler : IRequestHandler<GetOrderStatisticsQuery, OrderStatsDto>
{
    private readonly IDashboardService _dashboardService;

    public GetOrderStatisticsQueryHandler(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<OrderStatsDto> Handle(GetOrderStatisticsQuery request, CancellationToken cancellationToken)
    {
        return await _dashboardService.GetOrderStatisticsAsync(request.Date);
    }
}