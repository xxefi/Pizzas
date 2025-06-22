import { IRegisterData } from "@/app/core/interfaces/data/register.data";
import { handleApiError } from "../api/httpClient";
import { batchService } from "./batchService";

export const authService = {
  login: async (email: string, password: string) => {
    const requests = [
      {
        operation: "LoginCommand",
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

  register: async (register: IRegisterData): Promise<string | undefined> => {
    const requests = [
      {
        operation: "RegisterCommand",
        parameters: {
          register,
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

  confirmOtp: async (s: string, o: string) => {
    const requests = [
      {
        operation: "ConfirmOtpCommand",
        parameters: {
          s,
          o,
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
        operation: "LogoutCommand",
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
