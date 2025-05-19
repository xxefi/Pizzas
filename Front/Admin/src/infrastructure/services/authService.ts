import { handleApiError } from "../api/apiClient";
import { batchService } from "./batchService";

export const authService = {
  login: async (email: string, password: string) => {
    const requests = [
      {
        action: "LoginCommand",
        parameters: {
          login: {
            email,
            password,
          },
        },
      },
    ];

    try {
      const response = await batchService.execute(requests);
      return response[0];
    } catch (e) {
      handleApiError(e);
      return undefined;
    }
  },

  logout: async (): Promise<void> => {
    const requests = [
      {
        action: "LogoutCommand",
        parameters: {},
      },
    ];

    try {
      await batchService.execute(requests);
    } catch (e) {
      handleApiError(e);
    }
  },
};
