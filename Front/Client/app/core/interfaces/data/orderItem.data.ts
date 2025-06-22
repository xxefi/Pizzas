import { IPizzas } from "./pizzas.data";

export interface IOrderItem {
  id: string;
  orderId: string;
  pizzaId: string;
  pizza: IPizzas;
  quantity: number;
  price: number;
  createdAt: string;
  updatedAt: string | null;
}
