import type { CreatePizzaDto, UpdatePizzaDto } from "../../core/dtos";
import type { IPizzas } from "../../core/interfaces/data/pizzas.data";
import type { IPaginatedPizzasResponse } from "../../core/interfaces/response/paginatedPizzas.response";
import { handleApiError } from "../api/apiClient";
import { pizzaRequests } from "../requests/pizzaRequests";
import { batchService } from "./batchService";

export const pizzasService = {
  getPizzasPage: async (
    pageNumber: number,
    pageSize: number
  ): Promise<IPaginatedPizzasResponse> => {
    const requests = pizzaRequests.getPizzasPage(pageNumber, pageSize, "USD");
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
    const requests = pizzaRequests.getPizzaById(id, "USD");
    try {
      const response = await batchService.execute(requests);
      return response[0] || ({} as IPizzas);
    } catch (e) {
      handleApiError(e);
      return {} as IPizzas;
    }
  },

  searchPizza: async (searchTerm: string): Promise<IPizzas[]> => {
    const requests = pizzaRequests.searchPizza(searchTerm, "USD");
    try {
      const response = await batchService.execute(requests);
      return response[0] || [];
    } catch (e) {
      handleApiError(e);
      return [];
    }
  },
  createPizza: async (pizza: CreatePizzaDto): Promise<IPizzas | null> => {
    const requests = pizzaRequests.createPizza(pizza);
    try {
      const response = await batchService.execute(requests);
      return response[0] || null;
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },
  updatePizza: async (
    id: string,
    pizza: UpdatePizzaDto
  ): Promise<IPizzas | null> => {
    const requests = pizzaRequests.updatePizza(id, pizza);
    try {
      const response = await batchService.execute(requests);
      return response[0] || null;
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },
  deletePizza: async (id: string): Promise<boolean> => {
    const requests = pizzaRequests.deletePizza(id);
    try {
      const response = await batchService.execute(requests);
      return response[0] ?? false;
    } catch (e) {
      handleApiError(e);
      return false;
    }
  },
};
