import { IPizzas } from "../data/pizzas.data";

export interface IFavoriteStore {
  favorites: IPizzas[];
  favoritesPage: { addedAt: string; pizza: IPizzas }[];
  loading: boolean;
  error: string;
  currentPage: number;
  totalPages: number;
  totalItems: number;
  pageSize: number;
  setFavorites: (favorites: IPizzas[]) => void;
  setFavoritesPage: (
    favoritesPage: { addedAt: string; pizza: IPizzas }[]
  ) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string) => void;
  setCurrentPage: (page: number) => void;
  setTotalPages: (total: number) => void;
  setTotalItems: (total: number) => void;
  setPageSize: (size: number) => void;
  addFavorite: (pizza: IPizzas) => void;
  removeFavorite: (id: string) => void;
}
