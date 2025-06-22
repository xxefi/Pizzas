import type { PizzaSize } from "../../types/pizza.type";
import type { IPizzas } from "../data/pizzas.data";
import type { IPrices } from "../data/prices.data";

export interface IPizzaTableContentProps {
  pizzasPage: IPizzas[] | null;
  selectedSize: string | null;
  getPriceForSize: (prices: IPrices, size: PizzaSize) => void;
  getDiscountPercentage: (original: number, discount: number) => string;
  handleDeleteClick: (id: string) => void;
  setDetailsDrawer: (data: { open: boolean; pizza: IPizzas }) => void;
  t: (key: string) => string;
}
