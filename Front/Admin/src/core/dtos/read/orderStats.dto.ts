export interface OrderStatsDto {
  totalOrders: number;
  pendingOrders: number;
  completedOrders: number;
  cancelledOrders: number;
  averagePreparationTime: number;
  averageDeliveryTime: number;
}
