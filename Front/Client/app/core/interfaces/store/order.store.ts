import { IOrder } from "../data/order.data";

export interface IOrderStore {
  orders: IOrder[];
  setOrders: (orders: IOrder[]) => void;
  createOrder: (order: IOrder) => void;
}
