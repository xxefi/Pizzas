import type { IIngredient } from "./ingredient.data";
import type { IPrices } from "./prices.data";

export interface IPizzas {
  id: string;
  name: string;
  description: string;
  rating: number;
  imageUrl: string;
  stock: boolean;
  top: boolean;
  size: string;
  prices: IPrices[];
  ingredients: IIngredient[];
}
