import type { ICategory } from "../../core/interfaces/data/category.data";
import { handleApiError } from "../api/apiClient";
import { batchService } from "./batchService";

export const categoryService = {
  getCategories: async (): Promise<ICategory[]> => {
    const requests = [
      {
        operation: "GetCategoriesQuery",
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
  getPizzasByCategory: async (
    pageNumber: number,
    pageSize: number,
    categoryName: string
  ) => {
    const requests = [
      {
        operation: "GetPizzasByCategoryQuery",
        parameters: {
          pageNumber,
          pageSize,
          categoryName,
        },
      },
    ];
    try {
      const response = await batchService.execute(requests);
      const { data, totalItems, totalPages, currentPage } = response[0];
      return {
        data,
        totalItems,
        totalPages,
        currentPage,
        pageSize,
      };
    } catch (e) {
      handleApiError(e);
      return {
        data: [],
        totalItems: 0,
        totalPages: 0,
        currentPage: 1,
        pageSize,
      };
    }
  },
};
