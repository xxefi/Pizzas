import { IBasketItem } from "../data/basketItem.data";

export interface IBasketStore {
  items: IBasketItem[];
  loading: boolean;
  basketOpen: boolean;
  error: string;
  totalItems: number;
  totalPrice: number;
  setItems: (items: IBasketItem[]) => void;
  setLoading: (loading: boolean) => void;
  setBasketOpen: (basketOpen: boolean) => void;
  setError: (error: string) => void;
  setTotalItems: (total: number) => void;
  setTotalPrice: (total: number) => void;
  addItem: (item: IBasketItem) => void;
  removeItem: (id: string) => void;
  updateQuantity: (id: string, delta: number) => void;
}
