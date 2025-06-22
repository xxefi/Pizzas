import { handleApiError } from "../api/apiClient";
import { authRequests } from "../requests/authRequests";
import { batchService } from "./batchService";

export const authService = {
  login: async (email: string, password: string) => {
    const requests = authRequests.login(email, password);

    try {
      const response = await batchService.execute(requests);
      return response[0];
    } catch (e) {
      handleApiError(e);
      return undefined;
    }
  },

  logout: async (): Promise<void> => {
    const requests = authRequests.logout();

    try {
      await batchService.execute(requests);
    } catch (e) {
      handleApiError(e);
    }
  },
};
