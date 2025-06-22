import { handleApiError } from "../api/httpClient";
import { getCurrencyFromStorage } from "@/app/application/hooks/useCurrency";
import { batchService } from "./batchService";
import { IBasket } from "@/app/core/interfaces/data/basket.data";
import { basketRequests } from "../requests/basketRequests";

const { code: targetCurrency } = getCurrencyFromStorage();

export const basketService = {
  getBasket: async (): Promise<IBasket> => {
    try {
      const response = await batchService.execute(
        basketRequests.getBasket(targetCurrency)
      );
      return response[0] || ({} as IBasket);
    } catch (e) {
      handleApiError(e);
      return {} as IBasket;
    }
  },

  addItemToBasket: async (
    pizzaId: string,
    quantity: number,
    size: string
  ): Promise<void> => {
    try {
      await batchService.execute(
        basketRequests.addItemToBasket(pizzaId, quantity, targetCurrency, size)
      );
    } catch (e) {
      handleApiError(e);
    }
  },

  removeItemFromBasket: async (basketItemId: string): Promise<void> => {
    try {
      await batchService.execute(
        basketRequests.removeItemFromBasket(basketItemId, targetCurrency)
      );
    } catch (e) {
      handleApiError(e);
    }
  },

  updateItemQuantity: async (
    basketItemId: string,
    quantity: number
  ): Promise<void> => {
    try {
      await batchService.execute(
        basketRequests.updateItemQuantity(
          basketItemId,
          quantity,
          targetCurrency
        )
      );
    } catch (e) {
      handleApiError(e);
    }
  },

  getTotalPrice: async (): Promise<number> => {
    try {
      const response = await batchService.execute(
        basketRequests.getTotalPrice(targetCurrency)
      );
      return response[0] || 0;
    } catch (e) {
      handleApiError(e);
      return 0;
    }
  },

  clearBasket: async (): Promise<void> => {
    try {
      await batchService.execute(basketRequests.clearBasket(targetCurrency));
    } catch (e) {
      handleApiError(e);
    }
  },

  getBasketItemCount: async (): Promise<number> => {
    try {
      const response = await batchService.execute(
        basketRequests.getBasketItemCount(targetCurrency)
      );
      return response[0] || 0;
    } catch (e) {
      handleApiError(e);
      return 0;
    }
  },
};
