import type { OrderStatus } from "../../core/enums/orderStatus.enum";

export const orderRequests = {
  getOrders: () => [
    {
      operation: "GetOrdersQuery",
      parameters: {},
    },
  ],
  getOrderById: (orderId: string) => [
    {
      operation: "GetOrderQuery",
      parameters: { orderId },
    },
  ],
  updateOrderStatus: (orderId: string, newStatus: OrderStatus) => [
    {
      operation: "UpdateOrderStatusCommand",
      parameters: {
        orderId,
        newStatus,
      },
    },
  ],
};
