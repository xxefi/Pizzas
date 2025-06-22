import { BatchRequest } from "@/app/core/types/batchRequest.type";
import { handleApiError, httpClient } from "../api/httpClient";

export const batchService = {
  execute: async (requests: BatchRequest[]): Promise<any[]> => {
    try {
      const response = await httpClient.post("/api/Execute/b", {
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
