import type { IOrder } from "../data/order.data";

export interface IOrderStore {
  orders: IOrder[];
  selectedOrder: IOrder | null;
  loading: boolean;
  setOrders: (orders: IOrder[]) => void;
  setSelectedOrder: (order: IOrder | null) => void;
  setLoading: (loading: boolean) => void;
  updateOrderInList: (order: IOrder) => void;
}
