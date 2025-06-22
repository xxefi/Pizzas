using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Dashboard;

namespace Pizzas.RequestPipeline.Handlers.Dashboard.Queries;

public class GetTopSellingProductsQueryHandler : IRequestHandler<GetTopSellingProductsQuery, IEnumerable<TopProductDto>>
{
    private readonly IDashboardService _dashboardService;

    public GetTopSellingProductsQueryHandler(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IEnumerable<TopProductDto>> Handle(GetTopSellingProductsQuery request, CancellationToken cancellationToken)
    {
        return await _dashboardService.GetTopSellingProductsAsync(request.Limit);
    }
}