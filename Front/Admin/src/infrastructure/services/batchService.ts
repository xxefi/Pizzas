import type { BatchRequest } from "../../core/requests/batchRequest.type";
import { handleApiError, apiClient } from "../api/httpClient";

export const batchService = {
  execute: async (requests: BatchRequest[]): Promise<any[]> => {
    try {
      const response = await apiClient.post("/api/Universal/batchexecute", {
        requests: requests.map((req) => ({
          action: req.action,
          parameters: req.parameters,
        })),
      });

      return response.data.response;
    } catch (e) {
      handleApiError(e);
      return [];
    }
  },
};
