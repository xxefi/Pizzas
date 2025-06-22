import { IPizzas } from "@/app/core/interfaces/data/pizzas.data";
import { IFavoriteStore } from "@/app/core/interfaces/store/favorites.store";
import { create } from "zustand";

export const favoritesStore = create<IFavoriteStore>((set) => ({
  favorites: [],
  favoritesPage: [],
  loading: false,
  error: "",
  currentPage: 1,
  totalPages: 0,
  totalItems: 0,
  pageSize: 10,
  setFavorites: (favorites: IPizzas[]) => set({ favorites }),
  setFavoritesPage: (favoritesPage: { addedAt: string; pizza: IPizzas }[]) =>
    set({ favoritesPage }),
  setLoading: (loading: boolean) => set({ loading }),
  setError: (error: string) => set({ error }),
  setCurrentPage: (page: number) => set({ currentPage: page }),
  setTotalPages: (total: number) => set({ totalPages: total }),
  setTotalItems: (total: number) => set({ totalItems: total }),
  setPageSize: (size: number) => set({ pageSize: size }),

  addFavorite: (pizza: IPizzas) =>
    set((state) => ({
      favorites: [...state.favorites, pizza],
    })),

  removeFavorite: (id: string) =>
    set((state) => ({
      favorites: state.favorites.filter((pizza) => pizza.id !== id),
    })),
}));
