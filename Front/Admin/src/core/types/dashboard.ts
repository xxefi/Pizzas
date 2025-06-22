import type {
  RecentTransactionDto,
  SalesChartDto,
  TopProductDto,
} from "../dtos";

export interface SalesStats {
  totalSales: number;
  orderCount: number;
  averageOrderValue: number;
  refundAmount: number;
  paymentMethods: any[];
}

export interface OrderStats {
  totalOrders: number;
  pendingOrders: number;
  completedOrders: number;
  cancelledOrders: number;
  averagePreparationTime: number;
  averageDeliveryTime: number;
}

export interface CustomerSegment {
  name: string;
  count: number;
}

export interface CustomerStats {
  totalCustomers: number;
  newCustomers: number;
  returningCustomers: number;
  averageCustomerValue: number;
  segments: CustomerSegment[];
}

export interface DashboardStats {
  totalRevenue: number;
  totalOrders: number;
  totalCustomers: number;
  averageOrderValue: number;
  todayRevenue: number;
  todayOrders: number;
  monthlyRevenue: number;
  revenueGrowth: number;
}

export interface DashboardResponse {
  response: [
    SalesStats,
    OrderStats,
    CustomerStats,
    DashboardStats,
    RecentTransactionDto[],
    SalesChartDto[],
    TopProductDto[]
  ];
}
