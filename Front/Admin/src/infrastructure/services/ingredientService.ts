import type { IIngredient } from "../../core/interfaces/data/ingredient.data";
import { handleApiError } from "../api/apiClient";
import { batchService } from "./batchService";

export const ingredientService = {
  getAllIngredients: async (): Promise<IIngredient[]> => {
    const requests = [
      {
        operation: "GetAllIngredientsQuery",
        parameters: {},
      },
    ];

    try {
      const response = await batchService.execute(requests);
      return response.length ? response[0] : [];
    } catch (e) {
      handleApiError(e);
      return [];
    }
  },
};
