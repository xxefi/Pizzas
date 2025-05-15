import { IRegisterData } from "@/app/core/interfaces/data/register.data";
import { handleApiError, httpClient } from "../api/httpClient";
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

  register: async (newUser: IRegisterData): Promise<void> => {
    const requests = [
      {
        action: "RegisterCommand",
        parameters: {
          newUser,
        },
      },
    ];

    try {
      await batchService.execute(requests);
    } catch (e) {
      handleApiError(e);
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
