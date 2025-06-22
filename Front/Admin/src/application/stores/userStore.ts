import { create } from "zustand";
import type { IUserStore } from "../../core/interfaces/store/user.store";

export const userStore = create<IUserStore>((set) => ({
  users: [],
  totalItems: 0,
  totalPages: 0,
  currentPage: 1,
  pageSize: 10,
  loading: false,
  error: null,
  setUsers: (users) => set({ users }),
  setPagination: (totalItems, totalPages, currentPage, pageSize) =>
    set({ totalItems, totalPages, currentPage, pageSize }),
  setLoading: (loading) => set({ loading }),
  setError: (error) => set({ error }),
  addUser: (user) =>
    set((state) => ({
      users: [user, ...state.users],
      totalItems: state.totalItems + 1,
    })),
  updateUser: (user) =>
    set((state) => ({
      users: state.users.map((u) => (u.id === user.id ? user : u)),
    })),
  removeUser: (id) =>
    set((state) => ({
      users: state.users.filter((u) => u.id !== id),
      totalItems: state.totalItems > 0 ? state.totalItems - 1 : 0,
    })),
  clearError: () => set({ error: null }),
}));
