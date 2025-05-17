import { IRegisterData } from "@/app/core/interfaces/data/register.data";
import { handleApiError } from "../api/httpClient";
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

  register: async (newUser: IRegisterData): Promise<string | undefined> => {
    const requests = [
      {
        action: "RegisterCommand",
        parameters: {
          newUser,
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

  confirmOtp: async (sessionId: string, otp: string) => {
    const requests = [
      {
        action: "ConfirmOtpCommand",
        parameters: {
          sessionId,
          otp,
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
