import { create } from "zustand";
import type { IPizzasUIStore } from "../../core/interfaces/store/pizzasUI.store";

export const usePizzasUIStore = create<IPizzasUIStore>((set) => ({
  searchTerm: "",
  setSearchTerm: (term) => set({ searchTerm: term }),
  searchTimeout: null,
  setSearchTimeout: (timeout) => set({ searchTimeout: timeout }),
  selectedSize: "Medium",
  setSelectedSize: (size) => set({ selectedSize: size }),
  pizzaToDelete: null,
  setPizzaToDelete: (id) => set({ pizzaToDelete: id }),
  showDeleteModal: false,
  setShowDeleteModal: (value) => set({ showDeleteModal: value }),
  detailsDrawer: {
    open: false,
    pizza: null,
  },
  setDetailsDrawer: (value) => set({ detailsDrawer: value }),
}));
