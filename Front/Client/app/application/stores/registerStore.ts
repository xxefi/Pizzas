import { IFormData } from "@/app/core/interfaces/data/form.data";
import { IRegisterStore } from "@/app/core/interfaces/store/register.store";
import { create } from "zustand";

export const registerStore = create<IRegisterStore>((set) => ({
  formData: {
    username: "",
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirmPassword: "",
    agreeToTerms: false,
  },
  loading: false,
  error: "",
  setFormData: (formValue: Partial<IFormData>) =>
    set((state) => ({
      formData: {
        ...state.formData,
        ...formValue,
      },
    })),
  setLoading: (loading) => set({ loading }),
  setError: (error) => set({ error }),
  resetForm: () =>
    set({
      formData: {
        username: "",
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        confirmPassword: "",
        agreeToTerms: false,
      },
      loading: false,
      error: "",
    }),
}));
