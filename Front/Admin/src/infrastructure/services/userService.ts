import { batchService } from "./batchService";
import { handleApiError } from "../api/apiClient";
import type { IUser } from "../../core/interfaces/data/user.data";
import { userRequests } from "../requests/userRequests";
import type { IPaginatedUsersResponse } from "../../core/interfaces/response/paginatedUsers.response";
import type { UpdateUserDto } from "../../core/dtos/update/updateUser.dto";

export const userService = {
  createUser: async (userData: IUser): Promise<IUser | null> => {
    const requests = userRequests.createUser(userData);
    try {
      const response = await batchService.execute(requests);
      return response[0] || null;
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },

  updateUser: async (
    id: string,
    userData: UpdateUserDto
  ): Promise<IUser | null> => {
    const requests = userRequests.updateUser(id, userData);
    try {
      const response = await batchService.execute(requests);
      return response[0] || null;
    } catch (e) {
      handleApiError(e);
      return null;
    }
  },

  deleteUser: async (id: string): Promise<boolean> => {
    const requests = userRequests.deleteUser(id);
    try {
      const response = await batchService.execute(requests);
      return response[0] ?? false;
    } catch (e) {
      handleApiError(e);
      return false;
    }
  },

  getUsersPage: async (
    pageNumber: number,
    pageSize: number
  ): Promise<IPaginatedUsersResponse> => {
    const requests = userRequests.getUsersPage(pageNumber, pageSize);
    try {
      const response = await batchService.execute(requests);
      const { data, totalItems, totalPages, currentPage } = response[0] || {};

      return {
        data: data || [],
        totalItems: totalItems || 0,
        totalPages: totalPages || 0,
        currentPage: currentPage || 1,
        pageSize,
      };
    } catch (e) {
      handleApiError(e);
      return {
        data: [],
        totalItems: 0,
        totalPages: 0,
        currentPage: 1,
        pageSize,
      };
    }
  },
};
