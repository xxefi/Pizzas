import { IPizzas } from "../data/pizzas.data";

export interface IPizzassStore {
  pizzas: IPizzas[];
  pizza: IPizzas | null;
  popularPizzas: IPizzas[];
  newPizzas: IPizzas[];
  pizzasPage: IPizzas[];
  searchResults: IPizzas[];
  loading: boolean;
  popupOpen: boolean;
  currentPage: number;
  totalPages: number;
  totalItems: number;
  pageSize: number;
  error: string;
  setPizzas: (pizzas: IPizzas[]) => void;
  setPizza: (pizza: IPizzas | null) => void;
  setPopularPizzas: (pizzas: IPizzas[]) => void;
  setNewPizzas: (pizzas: IPizzas[]) => void;
  setPizzasPage: (pizzas: IPizzas[]) => void;
  setSearchResults: (pizzas: IPizzas[]) => void;
  setLoading: (loading: boolean) => void;
  setPopupOpen: (open: boolean) => void;
  setCurrentPage: (page: number) => void;
  setTotalPages: (pages: number) => void;
  setTotalItems: (items: number) => void;
  setPageSize: (size: number) => void;
  setError: (error: string) => void;
}
