import { IProfile } from "@/app/core/interfaces/data/profile.data";
import { handleApiError } from "../api/httpClient";
import { batchService } from "./batchService";

export const profileService = {
  getCurrentUser: async (): Promise<IProfile> => {
    const requests = [
      {
        operation: "GetCurrentUserQuery",
        parameters: {},
      },
    ];

    try {
      const response = await batchService.execute(requests);
      return response[0] as IProfile;
    } catch (error) {
      handleApiError(error);
      return {} as IProfile;
    }
  },
  /*
  getProfile: async (): Promise<IProfile> => {
    const { userId, setUserId } = userStore.getState();
    let id = userId;

    if (!id) {
      const token = Cookies.get("atk");

      if (token) {
        id = getUserIdFromToken(token);
        setUserId(id);
        console.log(id);
      }
    }

    try {
      const response = await httpClient.get(`/api/Users/id/${id}`);
      return response.data.data;
    } catch (error) {
      console.error("Error fetching profile:", error);
      throw error;
    }
  },

  updateProfile: async (data: Partial<IProfile>): Promise<IProfile> => {
    const { userId } = userStore.getState();
    let id = userId;

    if (!id) {
      const token = document.cookie
        .split("; ")
        .find((row) => row.startsWith("atk="))
        ?.split("=")[1];

      if (token) {
        id = getUserIdFromToken(token);
      }
    }

    try {
      const response = await httpClient.put(`/api/Users/update/id/${id}`, data);
      return response.data.data;
    } catch (error) {
      console.error("Error updating profile:", error);
      throw error;
    }
  },*/
};
