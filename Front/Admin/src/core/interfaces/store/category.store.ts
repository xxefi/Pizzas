import type { ICategory } from "../data/category.data";

export interface ICategoryStore {
  categories: ICategory[];
  loadingCategory: boolean;
  error: string | null;
  currentPage: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
  setCategories: (categories: ICategory[]) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string | null) => void;
  setCategoryPage: (categoryPage: ICategory[]) => void;
  setTotalPages: (totalPages: number) => void;
  setCurrentPage: (currentPage: number) => void;
  setPageSize: (pageSize: number) => void;
  setTotalItems: (totalItems: number) => void;
}
