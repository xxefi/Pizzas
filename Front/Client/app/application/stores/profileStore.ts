import { IProfileStore } from "@/app/core/interfaces/store/profile.store";
import { create } from "zustand";

export const profileStore = create<IProfileStore>((set) => ({
  profile: null,
  formData: null,
  loading: false,
  error: null,
  setProfile: (profile) => set({ profile, formData: profile }),
  setFormData: (formData) => set({ formData }),
  setLoading: (loading) => set({ loading }),
  setError: (error) => set({ error }),
}));
