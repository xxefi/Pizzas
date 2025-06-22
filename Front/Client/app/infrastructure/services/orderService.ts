import { IOrder } from "@/app/core/interfaces/data/order.data";
import { handleApiError } from "../api/httpClient";
import { batchService } from "./batchService";
import { orderRequests } from "../requests/orderRequests";
import { ICreateOrderRequest } from "@/app/core/requests/createOrder.request";

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

  createOrder: async (order: ICreateOrderRequest): Promise<IOrder> => {
    try {
      const response = await batchService.execute(
        orderRequests.createOrder(order)
      );
      return response[0] as IOrder;
    } catch (e) {
      handleApiError(e);
      throw e;
    }
  },
};
