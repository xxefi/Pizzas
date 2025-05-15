import { IPizzas } from "../data/pizzas.data";

export interface IFavoritesPageResponse {
  data: { addedAt: string; pizza: IPizzas }[];
  totalItems: number;
  totalPages: number;
  currentPage: number;
  pageSize: number;
}
