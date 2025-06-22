import type { IUser } from "../data/user.data";

export interface IPaginatedUsersResponse {
  data: IUser[];
  totalItems: number;
  totalPages: number;
  currentPage: number;
  pageSize: number;
}
