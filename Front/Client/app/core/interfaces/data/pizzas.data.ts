import { IIngredients } from "./ingredients.data";
import { IPrices } from "./prices.data";

export interface IPizzas {
  id: string;
  category: string;
  name: string;
  description: string;
  rating: number;
  imageUrl: string;
  stock: boolean;
  top: boolean;
  size: string;
  prices: IPrices[];
  ingredients: IIngredients[];
}
