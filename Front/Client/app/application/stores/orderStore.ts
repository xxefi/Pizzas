import { IOrder } from "@/app/core/interfaces/data/order.data";
import { IOrderStore } from "@/app/core/interfaces/store/order.store";
import { create } from "zustand";

export const orderStore = create<IOrderStore>((set) => ({
  orders: [],
  setOrders: (orders: IOrder[]) => set({ orders }),
  createOrder: (order: IOrder) =>
    set((state) => ({ orders: [...state.orders, order] })),
}));
