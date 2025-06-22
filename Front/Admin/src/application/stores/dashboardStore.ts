import { create } from "zustand";
import type { IDashboardStore } from "../../core/interfaces/store/dashboard.store";

export const dashboardStore = create<IDashboardStore>((set) => ({
  dashboardData: null,
  loading: false,
  error: "",
  setDashboardData: (data) => set({ dashboardData: data }),
  setLoading: (loading) => set({ loading }),
  setError: (error) => set({ error }),
}));
