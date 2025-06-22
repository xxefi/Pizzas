import { useEffect } from "react";
import { usePizzas } from "@/app/application/hooks/usePizzas";

export function useCategoryPizzas(slug: string) {
  const {
    pizzasPage,
    loading,
    fetchPizzasByCategory,
    currency,
    currentPage,
    totalPages,
    handleCategoryPageChange,
  } = usePizzas();

  useEffect(() => {
    if (slug) {
      fetchPizzasByCategory(slug);
    }
  }, [slug, fetchPizzasByCategory]);

  return {
    pizzasPage,
    loading,
    currency,
    currentPage,
    totalPages,
    handleCategoryPageChange,
  };
}
