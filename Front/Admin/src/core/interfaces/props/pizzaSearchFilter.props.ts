import type { PizzaSize } from "../../types/pizza.type";

export interface IPizzaSearchFilterProps {
  searchTerm: string;
  setSearchTerm: (term: string) => void;
  selectedSize: string | null;
  setSelectedSize: (size: PizzaSize | null) => void;
  searchPlaceholder: string;
  selectSizeLabel: string;
  t: (key: string) => string;
}
