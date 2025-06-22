import { useEffect } from "react";
import { orderStore } from "../stores/orderStore";
import { orderService } from "@/app/infrastructure/services/orderService";
import { ICreateOrderRequest } from "@/app/core/requests/createOrder.request";

export const useOrders = () => {
  const { orders, setOrders, createOrder: addToStore } = orderStore();

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const data = await orderService.getOrders();
        setOrders(data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchOrders();
  }, [setOrders]);

  const createOrder = async (orderData: ICreateOrderRequest) => {
    try {
      const newOrder = await orderService.createOrder(orderData);
      if (newOrder) {
        addToStore(newOrder);
      }
      return newOrder;
    } catch (error) {
      console.error(error);
    }
  };

  return {
    orders,
    createOrder,
  };
};
