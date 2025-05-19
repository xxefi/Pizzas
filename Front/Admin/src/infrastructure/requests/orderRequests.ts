import type { OrderStatus } from "../../core/enums/orderStatus.enum";

export const orderRequests = {
  getOrders: () => [
    {
      action: "GetOrdersQuery",
      parameters: {},
    },
  ],
  getOrderById: (orderId: string) => [
    {
      action: "GetOrderQuery",
      parameters: { orderId },
    },
  ],
  updateOrderStatus: (orderId: string, newStatus: OrderStatus) => [
    {
      action: "UpdateOrderStatusCommand",
      parameters: {
        orderId,
        newStatus,
      },
    },
  ],
};
