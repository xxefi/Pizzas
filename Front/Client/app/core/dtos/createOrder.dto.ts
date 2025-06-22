import { PaymentMethod } from "../enums/paymentMethod";
import { CreateOrderItemDto } from "./createOrderItem.dto";

export interface CreateOrderDto {
  addressId: string;
  paymentMethod: PaymentMethod;
  currency: string;
  items: CreateOrderItemDto[];
}
