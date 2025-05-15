import { getCurrencyFromStorage } from "@/app/application/hooks/useCurrency";
import { batchService } from "./batchService";
import { handleApiError } from "../api/httpClient";
import { IPaginatedPizzasResponse } from "@/app/core/interfaces/response/paginatedPizzas.response";
import { IPizzas } from "@/app/core/interfaces/data/pizzas.data";
import { pizzaRequests } from "@/app/infrastructure/requests/pizzaRequests";

const { code: targetCurrency } = getCurrencyFromStorage();

export const pizzasService = {
  getHomepageData: async () => {
    const requests = pizzaRequests.getHomepageData(targetCurrency);
    try {
      const [popularPizzas, newPizzas] = await batchService.execute(requests);
      return { popularPizzas, newPizzas };
    } catch (e) {
      handleApiError(e);
      return {
        popularPizzas: [],
        newPizzas: [],
      };
    }
  },

  getPizzasPage: async (
    pageNumber: number,
    pageSize: number
  ): Promise<IPaginatedPizzasResponse> => {
    const requests = pizzaRequests.getPizzasPage(
      pageNumber,
      pageSize,
      targetCurrency
    );
    try {
      const response = await batchService.execute(requests);
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

  getPizzaById: async (id: string): Promise<IPizzas> => {
    const requests = pizzaRequests.getPizzaById(id, targetCurrency);
    try {
      const response = await batchService.execute(requests);
      return response[0] || ({} as IPizzas);
    } catch (e) {
      handleApiError(e);
      return {} as IPizzas;
    }
  },

  searchPizza: async (searchTerm: string): Promise<IPizzas[]> => {
    const requests = pizzaRequests.searchPizza(searchTerm, targetCurrency);
    try {
      const response = await batchService.execute(requests);
      return response[0] || [];
    } catch (e) {
      handleApiError(e);
      return [];
    }
  },
};
