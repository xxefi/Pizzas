import { useEffect } from "react";
import { ingredientStore } from "../stores/ingredientStore";

export function useIngredients() {
  const { ingredients, loading, error, fetchIngredients } = ingredientStore();

  useEffect(() => {
    fetchIngredients();
  }, [fetchIngredients]);

  return { ingredients, loading, error };
}
