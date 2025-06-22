import type { PizzaSize } from "../../types/pizza.type";
import type { IPrices } from "../data/prices.data";

export interface IPizzaPriceCellProps {
  rowData: {
    prices: IPrices[];
  };
  selectedSize: PizzaSize | null;
  getPriceForSize: (
    prices: IPrices[],
    size: PizzaSize
  ) => { original: number; discount?: number } | null;
  getDiscountPercentage: (original: number, discount: number) => number;
  formatCurrency: (value: number) => string;
  t: (key: string) => string;
}
