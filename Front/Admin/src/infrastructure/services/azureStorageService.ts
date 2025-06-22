import { apiClient } from "../api/apiClient";

export const azureStorageService = {
  uploadFile: async (file: File): Promise<string> => {
    const formData = new FormData();
    formData.append("file", file);

    const response = await apiClient.post("/api/Upload", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return response.data.response.url;
  },
};
