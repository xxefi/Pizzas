import { IProfile } from "@/app/core/interfaces/data/profile.data";
import { handleApiError } from "../api/httpClient";
import { batchService } from "./batchService";

export const profileService = {
  getCurrentUser: async (): Promise<IProfile> => {
    const requests = [
      {
        action: "GetCurrentUserQuery",
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
  
};
