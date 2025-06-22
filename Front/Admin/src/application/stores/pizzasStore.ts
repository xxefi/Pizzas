import { create } from "zustand";
import type { IPizzasStore } from "../../core/interfaces/store/pizzas.store";

export const pizzasStore = create<IPizzasStore>((set) => ({
  pizzas: [],
  pizza: null,
  popularPizzas: [],
  newPizzas: [],
  pizzasPage: [],
  searchResults: [],
  loading: false,
  popupOpen: false,
  currentPage: 1,
  totalPages: 0,
  totalItems: 0,
  pageSize: 10,
  error: "",
  selectedPizzaId: null,
  editingPizza: null,
  isEditing: false,
  isCreating: false,
  setPizzas: (pizzas) => set({ pizzas }),
  setPizza: (pizza) => set({ pizza }),
  setPopularPizzas: (pizzas) => set({ popularPizzas: pizzas }),
  setNewPizzas: (pizzas) => set({ newPizzas: pizzas }),
  setPizzasPage: (pizzas) => set({ pizzasPage: pizzas }),
  setSearchResults: (pizzas) => set({ searchResults: pizzas }),
  setLoading: (loading) => set({ loading }),
  setPopupOpen: (open) => set({ popupOpen: open }),
  setCurrentPage: (page) => set({ currentPage: page }),
  setTotalPages: (pages) => set({ totalPages: pages }),
  setTotalItems: (items) => set({ totalItems: items }),
  setPageSize: (size) => set({ pageSize: size }),
  setError: (error) => set({ error }),
  setSelectedPizzaId: (id) => set({ selectedPizzaId: id }),
  setEditingPizza: (pizza) => set({ editingPizza: pizza }),
  setIsEditing: (editing) => set({ isEditing: editing }),
  setIsCreating: (creating) => set({ isCreating: creating }),
  createPizza: (pizza) =>
    set((state) => ({
      pizzas: [...state.pizzas, pizza],
      pizzasPage: [...state.pizzasPage, pizza],
    })),
  updatePizza: (pizza) =>
    set((state) => ({
      pizzas: state.pizzas.map((p) => (p.id === pizza.id ? pizza : p)),
      pizzasPage: state.pizzasPage.map((p) => (p.id === pizza.id ? pizza : p)),
    })),
  deletePizza: (id) =>
    set((state) => ({
      pizzas: state.pizzas.filter((p) => p.id !== id),
      pizzasPage: state.pizzasPage.filter((p) => p.id !== id),
    })),
}));
