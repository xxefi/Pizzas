import type { DashboardData } from "../../types/dashboardData.type";

export interface IDashboardStore {
  dashboardData: DashboardData | null;
  loading: boolean;
  error: string;
  setDashboardData: (data: DashboardData) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string) => void;
}
