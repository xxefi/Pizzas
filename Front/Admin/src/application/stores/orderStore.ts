import { create } from "zustand";
import type { IOrderStore } from "../../core/interfaces/store/order.store";

export const orderStore = create<IOrderStore>((set) => ({
  orders: [],
  selectedOrder: null,
  loading: false,

  setOrders: (orders) => set({ orders }),
  setSelectedOrder: (order) => set({ selectedOrder: order }),
  setLoading: (loading) => set({ loading }),
  updateOrderInList: (updatedOrder) =>
    set((state) => ({
      orders: state.orders.map((o) =>
        o.id === updatedOrder.id ? updatedOrder : o
      ),
      selectedOrder:
        state.selectedOrder?.id === updatedOrder.id
          ? updatedOrder
          : state.selectedOrder,
    })),
}));
