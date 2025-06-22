import type { PizzaSize } from "../../types/pizza.type";
import type { IPizzas } from "../data/pizzas.data";

export interface IPizzasUIStore {
  searchTerm: string;
  setSearchTerm: (term: string) => void;
  searchTimeout: NodeJS.Timeout | null;
  setSearchTimeout: (timeout: NodeJS.Timeout | null) => void;
  selectedSize: PizzaSize;
  setSelectedSize: (size: PizzaSize) => void;
  pizzaToDelete: string | null;
  setPizzaToDelete: (id: string | null) => void;
  showDeleteModal: boolean;
  setShowDeleteModal: (value: boolean) => void;
  detailsDrawer: {
    open: boolean;
    pizza: IPizzas | null;
  };
  setDetailsDrawer: (value: { open: boolean; pizza: IPizzas | null }) => void;
}
