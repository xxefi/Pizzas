import type { BatchRequest } from "../../core/requests/batchRequest.type";
import { handleApiError, apiClient } from "../api/apiClient";

export const batchService = {
  execute: async (requests: BatchRequest[]): Promise<any[]> => {
    try {
      const response = await apiClient.post("/api/Execute/b", {
        requests: requests.map((req) => ({
          operation: req.operation,
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
