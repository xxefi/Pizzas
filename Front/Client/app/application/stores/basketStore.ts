"use client";

import { IBasketItem } from "@/app/core/interfaces/data/basketItem.data";
import { IBasketStore } from "@/app/core/interfaces/store/basket.store";
import { create } from "zustand";

export const basketStore = create<IBasketStore>((set) => ({
  items: [],
  loading: false,
  basketOpen: false,
  error: "",
  totalItems: 0,
  totalPrice: 0,
  setItems: (items: IBasketItem[]) => set({ items }),
  setLoading: (loading: boolean) => set({ loading }),
  setBasketOpen: (basketOpen: boolean) => set({ basketOpen }),
  setError: (error: string) => set({ error }),
  setTotalItems: (total: number) => set({ totalItems: total }),
  setTotalPrice: (total: number) => set({ totalPrice: total }),
  addItem: (item: IBasketItem) =>
    set((state) => {
      const updatedItems = [...state.items, item];
      const updatedTotalPrice = updatedItems.reduce(
        (acc, item) => acc + item.price * item.quantity,
        0
      );
      return {
        items: updatedItems,
        totalPrice: updatedTotalPrice,
        totalItems: updatedItems.length,
      };
    }),
  removeItem: (id: string) =>
    set((state) => {
      const updatedItems = state.items.filter((item) => item.id !== id);
      const updatedTotalPrice = updatedItems.reduce(
        (acc, item) => acc + item.price * item.quantity,
        0
      );
      return {
        items: updatedItems,
        totalPrice: updatedTotalPrice,
        totalItems: updatedItems.length,
      };
    }),
  updateQuantity: (id: string, delta: number) =>
    set((state) => {
      const updatedItems = state.items.map((item) =>
        item.id === id
          ? { ...item, quantity: Math.max(1, item.quantity + delta) }
          : item
      );
      const updatedTotalPrice = updatedItems.reduce(
        (acc, item) => acc + item.price * item.quantity,
        0
      );
      return {
        items: updatedItems,
        totalPrice: updatedTotalPrice,
      };
    }),
}));
