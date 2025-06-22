import type { PizzaSize } from "../../types/pizza.type";

export interface CreatePizzaPriceDto {
  size: PizzaSize;
  originalPrice: number;
  discountPrice: number;
}
