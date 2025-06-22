import { PaymentMethod } from "../enums/paymentMethod";
import { ICreateOrderItemRequest } from "./createOrderItem.request";

export interface ICreateOrderRequest {
  addressId: string;
  paymentMethod: PaymentMethod;
  currency: string;
  items: ICreateOrderItemRequest[];
}
