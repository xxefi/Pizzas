import type { IUser } from "../data/user.data";

export interface IUserStore {
  users: IUser[];
  totalItems: number;
  totalPages: number;
  currentPage: number;
  pageSize: number;
  loading: boolean;
  error: string | null;
  setUsers: (users: IUser[]) => void;
  setPagination: (
    totalItems: number,
    totalPages: number,
    currentPage: number,
    pageSize: number
  ) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string | null) => void;
  addUser: (user: IUser) => void;
  updateUser: (user: IUser) => void;
  removeUser: (id: string) => void;
  clearError: () => void;
}
