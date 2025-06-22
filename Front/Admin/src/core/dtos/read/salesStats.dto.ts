export interface SalesStatsDto {
  totalSales: number;
  orderCount: number;
  averageOrderValue: number;
  refundAmount: number;
  paymentMethods: string[];
}
