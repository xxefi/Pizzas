import { IDeliveryInfo } from "./deliveryInfo.data";
import { IOrderItem } from "./orderItem.data";

export interface IOrder {
  id: string;
  orderNumber: string;
  status: string;
  paymentMethod: string;
  paymentStatus: string;
  trackingNumber: string;
  currency: string;
  subTotal: number;
  totalAmount: number;
  deliveryInfo: IDeliveryInfo;
  items: IOrderItem[];
  createdAt: string;
  updatedAt: string | null;
}
