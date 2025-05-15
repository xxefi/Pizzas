export const dashboardRequests = {
  getDashboardData: (
    startDate: string,
    endDate: string,
    chartPeriod: "Today" | "Week" | "Month" | "Year"
  ) => [
    {
      action: "GetSalesStatisticsQuery",
      parameters: { startDate, endDate },
    },
    {
      action: "GetOrderStatisticsQuery",
      parameters: { date: endDate },
    },
    {
      action: "GetCustomerStatisticsQuery",
      parameters: {},
    },
    {
      action: "GetDashboardStatisticsQuery",
      parameters: {},
    },
    {
      action: "GetRecentTransactionsQuery",
      parameters: { limit: 5 },
    },
    {
      action: "GetSalesChartDataQuery",
      parameters: { period: chartPeriod },
    },
    {
      action: "GetTopSellingProductsQuery",
      parameters: { limit: 10 },
    },
  ],
};
