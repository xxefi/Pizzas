import { getCurrencyFromStorage } from "@/app/application/hooks/useCurrency";
import { IPizzas } from "@/app/core/interfaces/data/pizzas.data";
import { batchService } from "./batchService";
import { handleApiError } from "../api/httpClient";
import { IFavoriteResponse } from "@/app/core/interfaces/response/favorite.response";
import { IFavoritesPageResponse } from "@/app/core/interfaces/response/paginatedFavorites.response";
import { favoriteRequests } from "../requests/favoriteRequests";

const { code: targetCurrency } = getCurrencyFromStorage();

export const favoritessService = {
  getFavorites: async (): Promise<IPizzas[]> => {
    try {
      const response = await batchService.execute(
        favoriteRequests.getFavorites(targetCurrency)
      );
      return (response[0] as IFavoriteResponse[]).map((item) => item.pizza);
    } catch (e) {
      handleApiError(e);
      return [];
    }
  },

  getFavoritesPage: async (
    pageNumber: number,
    pageSize: number
  ): Promise<IFavoritesPageResponse> => {
    try {
      const response = await batchService.execute(
        favoriteRequests.getFavoritesPage(pageNumber, pageSize, targetCurrency)
      );

      const { data, totalItems, totalPages, currentPage } = response[0] || {};

      return {
        data: data || [],
        totalItems: totalItems || 0,
        totalPages: totalPages || 0,
        currentPage: currentPage || 1,
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

  getFavoritesCount: async (): Promise<number> => {
    try {
      const response = await batchService.execute(
        favoriteRequests.getFavoritesCount()
      );
      return response[0] || 0;
    } catch (e) {
      handleApiError(e);
      return 0;
    }
  },

  addFavorite: async (pizzaId: string): Promise<void> => {
    try {
      await batchService.execute(
        favoriteRequests.addFavorite(pizzaId, targetCurrency)
      );
    } catch (e) {
      handleApiError(e);
    }
  },

  removeFavorite: async (pizzaId: string): Promise<void> => {
    try {
      await batchService.execute(favoriteRequests.removeFavorite(pizzaId));
    } catch (e) {
      handleApiError(e);
    }
  },
};
