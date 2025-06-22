using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Dashboard;

namespace Pizzas.RequestPipeline.Handlers.Dashboard.Queries;

public class GetSalesChartDataQueryHandler : IRequestHandler<GetSalesChartDataQuery, IEnumerable<SalesChartDto>>
{
    private readonly IDashboardService _dashboardService;

    public GetSalesChartDataQueryHandler(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IEnumerable<SalesChartDto>> Handle(GetSalesChartDataQuery request, CancellationToken cancellationToken)
    {
        return await _dashboardService.GetSalesChartDataAsync(request.Period);
    }
}