import { orderService } from "../../infrastructure/services/orderService";
import type { OrderStatus } from "../../core/enums/orderStatus.enum";
import { orderStore } from "../stores/orderStore";
import { toast } from "sonner";
import { useCallback, useEffect } from "react";

export const useOrderActions = () => {
  const { setOrders, setSelectedOrder, setLoading, updateOrderInList } =
    orderStore();

  const fetchOrders = useCallback(async () => {
    setLoading(true);
    try {
      const orders = await orderService.getOrders();
      setOrders(orders);
    } finally {
      setLoading(false);
    }
  }, [setLoading, setOrders]);

  const fetchOrderById = useCallback(
    async (id: string) => {
      setLoading(true);
      try {
        const order = await orderService.getOrderById(id);
        setSelectedOrder(order);
      } finally {
        setLoading(false);
      }
    },
    [setLoading, setSelectedOrder]
  );

  const updateOrderStatus = useCallback(
    async (id: string, status: OrderStatus) => {
      setLoading(true);
      try {
        const updatedOrder = await orderService.updateStatus(id, status);
        if (updatedOrder) {
          updateOrderInList(updatedOrder);
        }
      } catch (error: unknown) {
        const message = error instanceof Error ? error.message : String(error);
        toast.error(message);
      } finally {
        setLoading(false);
      }
    },
    [setLoading, updateOrderInList]
  );

  useEffect(() => {
    fetchOrders();
  }, [fetchOrders]);

  return {
    fetchOrders,
    fetchOrderById,
    updateOrderStatus,
  };
};
