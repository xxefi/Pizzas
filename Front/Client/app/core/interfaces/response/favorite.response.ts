import { IPizzas } from "../data/pizzas.data";

export interface IFavoriteResponse {
  id: string;
  addedAt: string;
  pizza: IPizzas;
}
