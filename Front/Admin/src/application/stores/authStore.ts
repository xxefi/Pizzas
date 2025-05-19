import { create } from "zustand";
import type { IAuthStore } from "../../core/interfaces/store/auth.store";

export const authStore = create<IAuthStore>((set) => ({
  profile: null,
  isAuthenticated: false,
  isInitialized: false,
  loading: false,
  error: "",
  email: "",
  password: "",
  user: null,
  setUser: (user) => set({ user }),
  setProfile: (profile) => set({ profile }),
  setEmail: (email) => set({ email }),
  setPassword: (password) => set({ password }),
  setLoading: (loading) => set({ loading }),
  setError: (error) => set({ error }),
  setInitialized: (isInitialized: boolean) => set({ isInitialized }),
  setAuthenticated: (isAuthenticated: boolean) => set({ isAuthenticated }),
}));
