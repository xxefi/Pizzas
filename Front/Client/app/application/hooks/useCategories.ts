import { useEffect } from "react";
import { useCategoryStore } from "@/app/application/stores/categoryStore";
import { categoryService } from "@/app/infrastructure/services/categoryService";
import { toast } from "sonner";

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
