using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetDashboardStatisticsAsync();
    Task<SalesStatsDto> GetSalesStatisticsAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<TopProductDto>> GetTopSellingProductsAsync(int limit = 10);
    Task<CustomerStatsDto> GetCustomerStatisticsAsync();
    Task<OrderStatsDto> GetOrderStatisticsAsync(DateTime? date = null);
    Task<IEnumerable<SalesChartDto>> GetSalesChartDataAsync(ChartPeriod period);
    Task<IEnumerable<RecentTransactionDto>> GetRecentTransactionsAsync(int limit = 5);
    bool IsSameWeek(DateTime date1, DateTime date2);
    DateTime GetWeekStartDate(DateTime date);
}