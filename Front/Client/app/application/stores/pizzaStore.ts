import { IPizzaStore } from "@/app/core/interfaces/store/pizza.store";
import { create } from "zustand";

export const usePizzaStore = create<IPizzaStore>((set) => ({
  pizzas: [],
  pizza: null,
  loading: true,
  error: null,
  copied: false,
  setPizzas: (pizzas) => set({ pizzas }),
  setPizza: (pizza) => set({ pizza }),
  setLoading: (loading) => set({ loading }),
  setError: (error) => set({ error }),
  setCopied: (copied) => set({ copied }),
  resetState: () =>
    set({ pizzas: [], pizza: null, loading: true, error: null, copied: false }),
}));
