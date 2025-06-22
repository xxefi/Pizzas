import type { PizzaSize } from "../../types/pizza.type";

export interface UpdatePizzaPriceDto {
  size?: PizzaSize;
  originalPrice?: number;
  discountPrice?: number;
}
