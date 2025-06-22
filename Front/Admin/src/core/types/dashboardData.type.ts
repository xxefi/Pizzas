import type {
  CustomerStatsDto,
  DashboardStatsDto,
  OrderStatsDto,
  RecentTransactionDto,
  SalesChartDto,
  SalesStatsDto,
  TopProductDto,
} from "../../core/dtos";

export type DashboardData = [
  SalesStatsDto,
  OrderStatsDto,
  CustomerStatsDto,
  DashboardStatsDto,
  RecentTransactionDto[],
  SalesChartDto[],
  TopProductDto[]
];
