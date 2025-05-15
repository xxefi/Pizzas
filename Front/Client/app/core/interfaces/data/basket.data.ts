import { IBasketItem } from "./basketItem.data";

export interface IBasket {
  items: IBasketItem[];
  totalItems: number;
  totalPrice: number;
}
