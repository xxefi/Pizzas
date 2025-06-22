import { toast } from "sonner";
import { useCategoryStore } from "../stores/categoryStore";
import { categoryService } from "../../infrastructure/services/categoryService";
import { useEffect } from "react";

export const useCategories = () => {
  const { categories, loadingCategory, setCategories, setLoading, setError } =
    useCategoryStore();

  useEffect(() => {
    const fetchCategories = async () => {
      setLoading(true);
      setError(null);
      try {
        const categories = await categoryService.getCategories();
        setCategories(categories);
      } catch (error: unknown) {
        if (error instanceof Error) {
          setError(error.message);
          toast.error(error.message);
        }
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, [setCategories, setLoading, setError]);

  return {
    categories,
    loadingCategory,
    error: useCategoryStore.getState().error,
  };
};
