using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.Application.Services.Main;

public class DashboardService : IDashboardService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public DashboardService(IOrderRepository orderRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }
    
    public async Task<DashboardStatsDto> GetDashboardStatisticsAsync()
    {
        var now = DateTime.UtcNow;
        var today = now.Date;
        var monthStart = new DateTime(now.Year, now.Month, 1);
        var previousMonthStart = monthStart.AddMonths(-1);
        var previousMonthEnd = monthStart.AddDays(-1);

        var allOrders = await _orderRepository.FindAsync(_ => true);

        var totalRevenue = allOrders.Sum(o => o.TotalAmount);
        var totalOrders = allOrders.Count;
        var avgOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

        var todayOrders = allOrders.Where(o => o.CreatedAt.Date == today);
        var todayRevenue = todayOrders.Sum(o => o.TotalAmount);

        var monthlyOrders = allOrders.Where(o => o.CreatedAt >= monthStart && o.CreatedAt <= now);
        var monthlyRevenue = monthlyOrders.Sum(o => o.TotalAmount);

        var previousMonthOrders = allOrders.Where(o => o.CreatedAt >= previousMonthStart && o.CreatedAt <= previousMonthEnd);
        var previousMonthRevenue = previousMonthOrders.Sum(o => o.TotalAmount);

        decimal revenueGrowth = 0;
        
        if (previousMonthRevenue > 0)
            revenueGrowth = (monthlyRevenue - previousMonthRevenue) / previousMonthRevenue * 100;

        var totalCustomers = (await _userRepository.FindAsync(_ => true)).Count;
            
        return new DashboardStatsDto
        {
            TotalRevenue = totalRevenue,
            TotalOrders = totalOrders,
            TotalCustomers = totalCustomers,
            AverageOrderValue = avgOrderValue,
            TodayOrders = todayOrders.Count(),
            TodayRevenue = todayRevenue,
            MonthlyRevenue = monthlyRevenue,
            RevenueGrowth = revenueGrowth
        };
    }

    public async Task<SalesStatsDto> GetSalesStatisticsAsync(DateTime startDate, DateTime endDate)
    {
        var startDateUtc = startDate.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(startDate, DateTimeKind.Utc) : startDate.ToUniversalTime();
        var endDateUtc = endDate.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(endDate, DateTimeKind.Utc) : endDate.ToUniversalTime();
        
        var allOrders = await _orderRepository.FindAsync(order =>
            order.CreatedAt >= startDateUtc &&
            order.CreatedAt <= endDateUtc
        );
        
        var completedOrders = allOrders.Where(o => o.Status == OrderStatus.Completed).ToList();
        var cancelledOrders = allOrders.Where(o => o.Status == OrderStatus.Canceled).ToList();
        
        var totalSales = completedOrders.Sum(o => o.TotalAmount);
        var orderCount = completedOrders.Count;
        var averageOrderValue = orderCount > 0 ? totalSales / orderCount : 0;
        
        var refundAmount = cancelledOrders.Sum(o => o.TotalAmount);

        var paymentMethodStats = completedOrders
            .GroupBy(o => o.PaymentMethod)
            .Select(g => new PaymentMethodStatsDto
            {
                Method = g.Key.ToString(),
                Count = g.Count(),
                Amount = g.Sum(o => o.TotalAmount)
            }).ToList();
        
        return new SalesStatsDto
        {
            TotalSales = totalSales,
            OrderCount = orderCount,
            AverageOrderValue = averageOrderValue,
            RefundAmount = refundAmount,
            PaymentMethods = paymentMethodStats
        };
        
    }

    public async Task<IEnumerable<TopProductDto>> GetTopSellingProductsAsync(int limit = 10)
    {
        var topProducts = await _orderRepository
            .FindAsync(o => o.Items.Any())  
            .ContinueWith(t => t.Result
                .SelectMany(o => o.Items)
                .GroupBy(i => i.PizzaId)  
                .OrderByDescending(g => g.Sum(i => i.Quantity))
                .Take(limit) 
                .Select(g => new TopProductDto
                {
                    ProductId = int.TryParse(g.Key, out var id) ? id : 0,
                    Name = g.First().Pizza.Name,
                    QuantitySold = g.Sum(i => i.Quantity),
                    Revenue = g.Sum(i => i.Quantity * i.Price),
                    ImageUrl = g.First().Pizza.ImageUrl,
                    StockLevel = g.First().Pizza.Stock ? 1 : 0
                })
                .ToList());

        return topProducts;
    }

    public async Task<CustomerStatsDto> GetCustomerStatisticsAsync()
    {
        var allUsers = await _userRepository.GetAllAsync();
        
        int totalCustomers = allUsers.Count();
        int newCustomers = allUsers.Count(u => u.CreatedAt >= DateTime.UtcNow.AddMonths(-1));
        int returningCustomers = allUsers.Count(u => u.Orders.Any(o => o.CreatedAt > u.CreatedAt));
        
        decimal averageCustomerValue = allUsers.Any() 
            ? allUsers.Average(u => u.Orders.Sum(o => o.TotalAmount)
                                    / (u.Orders.Count > 0 ? u.Orders.Count : 1))
            : 0;
        
        var customerStatsDto = new CustomerStatsDto
        {
            TotalCustomers = totalCustomers,
            NewCustomers = newCustomers,
            ReturningCustomers = returningCustomers,
            AverageCustomerValue = averageCustomerValue,
            Segments = new List<CustomerSegmentDto>
            {
                new() { Name = "newCustomers", Count = newCustomers },
                new() { Name = "returningCustomers", Count = returningCustomers }
            }
        };
        return customerStatsDto;
    }

    public async Task<OrderStatsDto> GetOrderStatisticsAsync(DateTime? date = null)
    {
        DateTime targetDate = date ?? DateTime.UtcNow;
        var ordersQuery = _orderRepository.GetAllAsync();
        
        var orders = await ordersQuery;
        
        if (date.HasValue)
            orders = orders.Where(o => o.CreatedAt.Date == targetDate.Date);
        
        
        int totalOrders = orders.Count();
        int pendingOrders = orders.Count(o => o.Status == OrderStatus.Pending);
        int completedOrders = orders.Count(o => o.Status == OrderStatus.Completed);
        int cancelledOrders = orders.Count(o => o.Status == OrderStatus.Canceled);
        
        
        var orderStatsDto = new OrderStatsDto
        {
            TotalOrders = totalOrders,
            PendingOrders = pendingOrders,
            CompletedOrders = completedOrders,
            CancelledOrders = cancelledOrders,
            AveragePreparationTime = 0,
            AverageDeliveryTime = 0
        };

        return orderStatsDto;
    }

    public async Task<IEnumerable<SalesChartDto>> GetSalesChartDataAsync(ChartPeriod period)
    {
        var orders = await _orderRepository.GetAllAsync();
        var users = await _userRepository.GetAllAsync();
        
        int returningCustomers = users.Count(u => u.Orders.Any(o => o.CreatedAt > u.CreatedAt));
        
        IEnumerable<SalesChartDto> salesData = period switch
        {
            ChartPeriod.Today => orders
                .Where(o => o.CreatedAt.Date == DateTime.UtcNow.Date) 
                .GroupBy(o => o.CreatedAt.Date)  
                .Select(g => new SalesChartDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.TotalAmount),
                    Customers = returningCustomers,
                    OrderCount = g.Count()
                }),

            ChartPeriod.Week => orders
                .Where(o => IsSameWeek(o.CreatedAt, DateTime.UtcNow)) 
                .GroupBy(o => GetWeekStartDate(o.CreatedAt))  
                .Select(g => new SalesChartDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.TotalAmount),
                    Customers = returningCustomers,
                    OrderCount = g.Count()
                }),

            ChartPeriod.Month => orders
                .Where(o => o.CreatedAt.Month == DateTime.UtcNow.Month && o.CreatedAt.Year == DateTime.UtcNow.Year)
                .GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month }) 
                .Select(g => new SalesChartDto
                {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Revenue = g.Sum(o => o.TotalAmount),
                    Customers = returningCustomers,
                    OrderCount = g.Count()
                }),

            ChartPeriod.Year => orders
                .Where(o => o.CreatedAt.Year == DateTime.UtcNow.Year) 
                .GroupBy(o => o.CreatedAt.Year) 
                .Select(g => new SalesChartDto
                {
                    Date = new DateTime(g.Key, 1, 1), 
                    Revenue = g.Sum(o => o.TotalAmount),
                    Customers = returningCustomers,
                    OrderCount = g.Count()
                }),

            _ => Enumerable.Empty<SalesChartDto>()
        };

        return salesData;
    }

    public async Task<IEnumerable<RecentTransactionDto>> GetRecentTransactionsAsync(int limit = 5)
    {
        var orders = await _orderRepository.FindAsync(o => true);
        
        var recentOrders = orders
            .OrderByDescending(o => o.CreatedAt)
            .Take(limit)
            .Select(o => new RecentTransactionDto
            {
                OrderId = o.Id,
                CustomerName = o.User.Username,
                Amount = o.TotalAmount,
                Date = o.CreatedAt,
                Status = o.Status.ToString(),
                PaymentMethod = o.PaymentMethod.ToString()
            });

        return recentOrders;
    }

    public bool IsSameWeek(DateTime date1, DateTime date2)
    {
        var diff1 = date1.Date.AddDays(-(int)date1.DayOfWeek + (int)DayOfWeek.Monday);
        var diff2 = date2.Date.AddDays(-(int)date2.DayOfWeek + (int)DayOfWeek.Monday);
        return diff1 == diff2;
    }

    public DateTime GetWeekStartDate(DateTime date)
    {
        int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.Date.AddDays(-1 * diff);
    }
}