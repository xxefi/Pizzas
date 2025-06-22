using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Dashboard;

namespace Pizzas.RequestPipeline.Handlers.Dashboard.Queries;

public class GetCustomerStatisticsQueryHandler : IRequestHandler<GetCustomerStatisticsQuery, CustomerStatsDto>
{
    private readonly IDashboardService _dashboardService;

    public GetCustomerStatisticsQueryHandler(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<CustomerStatsDto> Handle(GetCustomerStatisticsQuery request, CancellationToken cancellationToken)
    {
        return await _dashboardService.GetCustomerStatisticsAsync();
    }
}