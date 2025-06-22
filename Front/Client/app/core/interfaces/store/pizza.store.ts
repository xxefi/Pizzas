import { IPizzas } from "../data/pizzas.data";

export interface IPizzaStore {
  pizzas: IPizzas[];
  pizza: IPizzas | null;
  loading: boolean;
  error: string | null;
  copied: boolean;
  setPizzas: (pizzas: IPizzas[]) => void;
  setPizza: (pizza: IPizzas | null) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string | null) => void;
  setCopied: (copied: boolean) => void;
  resetState: () => void;
}
