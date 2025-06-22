import { IAuthStore } from "@/app/core/interfaces/store/auth.store";
import { create } from "zustand";

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
