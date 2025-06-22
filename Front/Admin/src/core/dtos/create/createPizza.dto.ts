import type { PizzaSize } from "../../types/pizza.type";
import type { CreateIngredientDto } from "./createIngredient.dto";
import type { CreatePizzaPriceDto } from "./createPizzaPrice.dto";

export interface CreatePizzaDto {
  categoryName: string;
  name: string;
  description: string;
  rating: number;
  imageUrl: string;
  stock: boolean;
  top: boolean;
  size: PizzaSize;
  ingredients: CreateIngredientDto[];
  prices: CreatePizzaPriceDto[];
}
