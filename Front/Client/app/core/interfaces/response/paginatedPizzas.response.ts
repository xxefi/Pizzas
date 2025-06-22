import { IPizzas } from "../data/pizzas.data";

export interface IPaginatedPizzasResponse {
  data: IPizzas[];
  totalItems: number;
  totalPages: number;
  currentPage: number;
  pageSize: number;
}
