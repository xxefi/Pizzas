import { create } from "zustand";
import type { ICategoryStore } from "../../core/interfaces/store/category.store";

export const useCategoryStore = create<ICategoryStore>((set) => ({
  categories: [],
  loadingCategory: false,
  error: null,
  currentPage: 1,
  pageSize: 10,
  totalItems: 0,
  totalPages: 0,
  setCategories: (categories) => set({ categories }),
  setLoading: (loading) => set({ loadingCategory: loading }),
  setError: (error) => set({ error }),
  setCategoryPage: (categoryPage) => set({ categories: categoryPage }),
  setTotalPages: (totalPages) => set({ totalPages }),
  setCurrentPage: (currentPage) => set({ currentPage }),
  setPageSize: (pageSize) => set({ pageSize }),
  setTotalItems: (totalItems) => set({ totalItems }),
}));
