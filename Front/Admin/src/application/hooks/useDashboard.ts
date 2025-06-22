import { useCallback, useEffect } from "react";
import { dashboardStore } from "../stores/dashboardStore";
import { dashboardService } from "../../infrastructure/services/dashboardService";
import { useHandleError } from "./useHandleError";
import type { SalesStatsDto } from "../../core/dtos/read/salesStats.dto";
import type { OrderStatsDto } from "../../core/dtos/read/orderStats.dto";
import type { CustomerStatsDto } from "../../core/dtos/read/customerStats.dto";
import type { DashboardStatsDto } from "../../core/dtos/read/dashboardStats.dto";
import type { RecentTransactionDto } from "../../core/dtos/read/recentTransaction.dto";
import type { SalesChartDto } from "../../core/dtos/read/salesChart.dto";
import type { TopProductDto } from "../../core/dtos/read/topProduct.dto";

export const useDashboard = () => {
  const {
    setDashboardData,
    setLoading,
    setError,
    dashboardData,
    loading,
    error,
  } = dashboardStore();

  const handleError = useHandleError(setError);

  const fetchDashboardData = useCallback(
    async (
      startDate: string,
      endDate: string,
      chartPeriod: "Today" | "Week" | "Month" | "Year"
    ) => {
      setLoading(true);
      setError("");
      try {
        const response = await dashboardService.getDashboardData(
          startDate,
          endDate,
          chartPeriod
        );

        const responseArray = [
          response.salesStats,
          response.orderStats,
          response.customerStats,
          response.dashboardStats,
          response.recentTransaction,
          response.salesChart,
          response.topProducts,
        ] as [
          SalesStatsDto,
          OrderStatsDto,
          CustomerStatsDto,
          DashboardStatsDto,
          RecentTransactionDto[],
          SalesChartDto[],
          TopProductDto[]
        ];

        setDashboardData(responseArray);
      } catch (error) {
        handleError(error);
      } finally {
        setLoading(false);
      }
    },
    [setLoading, setError, setDashboardData, handleError]
  );

  useEffect(() => {
    const today = new Date();
    const startDate = new Date(today.getFullYear(), today.getMonth(), 1)
      .toISOString()
      .split("T")[0];
    const endDate = today.toISOString().split("T")[0];
    fetchDashboardData(startDate, endDate, "Month");
  }, [fetchDashboardData]);

  return {
    dashboardData,
    loading,
    error,
  };
};
