import type { PizzaSize } from "../../types/pizza.type";
import type { UpdateIngredientDto } from "./updateIngredient.dto";
import type { UpdatePizzaPriceDto } from "./updatePizzaPrice.dto";

export interface UpdatePizzaDto {
  name?: string;
  category?: string;
  description?: string;
  rating?: number;
  imageUrl?: string;
  stock?: boolean;
  top?: boolean;
  size?: PizzaSize;
  ingredients?: UpdateIngredientDto[];
  prices?: UpdatePizzaPriceDto[];
}
