import { handleApiError } from "../api/httpClient";
import { reviewRequests } from "../requests/reviewRequests";
import { batchService } from "./batchService";

export const reviewService = {
  async fetchReviews(pizzaId: string) {
    try {
      const response = await batchService.execute(
        reviewRequests.getReviewsByBook(pizzaId)
      );
      return response[0];
    } catch (e) {
      handleApiError(e);
      return [];
    }
  },

  async addReview(pizzaId: string, content: string, rating: number) {
    try {
      const response = await batchService.execute(
        reviewRequests.createReview(pizzaId, content, rating)
      );
      return response[0];
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },

  async updateReview(id: string, content: string, rating: number) {
    try {
      const response = await batchService.execute(
        reviewRequests.updateReview(id, content, rating)
      );
      return response[0];
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },

  async deleteReview(id: string) {
    try {
      const response = await batchService.execute(
        reviewRequests.deleteReview(id)
      );
      return response[0];
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },
};
