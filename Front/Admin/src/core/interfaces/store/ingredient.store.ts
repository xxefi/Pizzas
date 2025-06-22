import type { IIngredient } from "../data/ingredient.data";

export interface IIngredientStore {
  ingredients: IIngredient[];
  loading: boolean;
  error: string | null;
  fetchIngredients: () => Promise<void>;
}
