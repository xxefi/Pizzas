import { IPizzaPrice } from "./pizzaPrice.data";

export interface IPizza {
  id: string;
  name: string;
  description: string;
  imageUrl: string;
  prices: IPizzaPrice[];
}
