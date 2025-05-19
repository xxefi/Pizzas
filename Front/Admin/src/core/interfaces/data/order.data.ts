import type { OrderStatus } from "../../enums/orderStatus.enum";
import type { IOrderItem } from "./orderItem.data";

export interface IOrder {
  id: string;
  orderNumber: string;
  status: OrderStatus;
  paymentMethod: string;
  paymentStatus: string;
  trackingNumber: string;
  currency: string;
  subTotal: number;
  totalAmount: number;
  deliveryInfo: any | null;
  items: IOrderItem[];
  createdAt: string;
  updatedAt: string | null;
}
