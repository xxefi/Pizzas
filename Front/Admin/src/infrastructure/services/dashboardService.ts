import type {
  CustomerStatsDto,
  DashboardStatsDto,
  OrderStatsDto,
  RecentTransactionDto,
  SalesChartDto,
  SalesStatsDto,
  TopProductDto,
} from "../../core/dtos";
import { handleApiError } from "../api/apiClient";
import { dashboardRequests } from "../requests/dashboardRequests";
import { batchService } from "./batchService";

export const dashboardService = {
  getDashboardData: async (
    startDate: string,
    endDate: string,
    chartPeriod: "Today" | "Week" | "Month" | "Year"
  ): Promise<{
    salesStats: SalesStatsDto;
    orderStats: OrderStatsDto;
    customerStats: CustomerStatsDto;
    dashboardStats: DashboardStatsDto;
    recentTransaction: RecentTransactionDto[];
    salesChart: SalesChartDto[];
    topProducts: TopProductDto[];
  }> => {
    try {
      const [
        salesStats,
        orderStats,
        customerStats,
        dashboardStats,
        recentTransaction,
        salesChart,
        topProducts,
      ] = await batchService.execute(
        dashboardRequests.getDashboardData(startDate, endDate, chartPeriod)
      );

      return {
        salesStats,
        orderStats,
        customerStats,
        dashboardStats,
        recentTransaction,
        salesChart,
        topProducts,
      };
    } catch (error) {
      handleApiError(error);
      return {
        salesStats: {} as SalesStatsDto,
        orderStats: {} as OrderStatsDto,
        customerStats: {} as CustomerStatsDto,
        dashboardStats: {} as DashboardStatsDto,
        recentTransaction: [],
        salesChart: [],
        topProducts: [],
      };
    }
  },
};
