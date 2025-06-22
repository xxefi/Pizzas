import { create } from "zustand";
import type { IIngredientStore } from "../../core/interfaces/store/ingredient.store";
import { ingredientService } from "../../infrastructure/services/ingredientService";

export const ingredientStore = create<IIngredientStore>((set) => ({
  ingredients: [],
  loading: false,
  error: null,

  fetchIngredients: async () => {
    set({ loading: true, error: null });
    try {
      const data = await ingredientService.getAllIngredients();
      set({ ingredients: data, loading: false });
    } catch (error: any) {
      set({
        error: error?.message || "",
        loading: false,
      });
    }
  },
}));
