import type { OrderStatus } from "../../core/enums/orderStatus.enum";
import type { IOrder } from "../../core/interfaces/data/order.data";
import { handleApiError } from "../api/apiClient";
import { orderRequests } from "../requests/orderRequests";
import { batchService } from "./batchService";

export const orderService = {
  getOrders: async (): Promise<IOrder[]> => {
    try {
      const response = await batchService.execute(orderRequests.getOrders());
      return response[0] || [];
    } catch (e) {
      handleApiError(e);
      return [];
    }
  },

  getOrderById: async (orderId: string): Promise<IOrder | null> => {
    try {
      const response = await batchService.execute(
        orderRequests.getOrderById(orderId)
      );
      return response[0] || null;
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },
  updateStatus: async (
    orderId: string,
    newStatus: OrderStatus
  ): Promise<IOrder | null> => {
    try {
      const response = await batchService.execute(
        orderRequests.updateOrderStatus(orderId, newStatus)
      );
      return response[0] || null;
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },
};
