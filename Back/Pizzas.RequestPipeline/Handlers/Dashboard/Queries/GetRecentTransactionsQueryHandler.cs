using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Dashboard;

namespace Pizzas.RequestPipeline.Handlers.Dashboard.Queries;

public class GetRecentTransactionsQueryHandler : IRequestHandler<GetRecentTransactionsQuery, IEnumerable<RecentTransactionDto>>
{
    private readonly IDashboardService _dashboardService;

    public GetRecentTransactionsQueryHandler(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IEnumerable<RecentTransactionDto>> Handle(GetRecentTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _dashboardService.GetRecentTransactionsAsync(request.Limit);
    }
}