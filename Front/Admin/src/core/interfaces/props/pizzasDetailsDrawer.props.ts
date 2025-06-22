import type { PizzaSize } from "../../types/pizza.type";
import type { IPizzas } from "../data/pizzas.data";
import type { IPrices } from "../data/prices.data";

export interface IPizzaDetailsDrawerProps {
  detailsDrawer: {
    open: boolean;
    pizza: IPizzas | null;
  };
  setDetailsDrawer: (data: { open: boolean; pizza: IPizzas | null }) => void;
  getPriceForSize: (prices: IPrices[], size: PizzaSize) => void;
  getDiscountPercentage: (original: number, discount: number) => string;
  t: (key: string) => string;
}
