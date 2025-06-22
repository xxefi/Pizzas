import type { PizzaSize } from "../../types/pizza.type";
import type { IPizzas } from "../data/pizzas.data";
import type { IPrices } from "../data/prices.data";

export interface IPizzaPricingSectionProps {
  pizza: {
    prices: IPrices[];
  } & IPizzas;
  getPriceForSize: (
    prices: IPrices[],
    size: PizzaSize
  ) => { original: number; discount?: number } | null;
  getDiscountPercentage: (original: number, discount: number) => number;
  t: (key: string) => string;
}
