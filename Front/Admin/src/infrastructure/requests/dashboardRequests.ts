export const dashboardRequests = {
  getDashboardData: (
    startDate: string,
    endDate: string,
    chartPeriod: "Today" | "Week" | "Month" | "Year"
  ) => [
    {
      operation: "GetSalesStatisticsQuery",
      parameters: { startDate, endDate },
    },
    {
      operation: "GetOrderStatisticsQuery",
      parameters: { date: endDate },
    },
    {
      operation: "GetCustomerStatisticsQuery",
      parameters: {},
    },
    {
      operation: "GetDashboardStatisticsQuery",
      parameters: {},
    },
    {
      operation: "GetRecentTransactionsQuery",
      parameters: { limit: 5 },
    },
    {
      operation: "GetSalesChartDataQuery",
      parameters: { period: chartPeriod },
    },
    {
      operation: "GetTopSellingProductsQuery",
      parameters: { limit: 10 },
    },
  ],
};
