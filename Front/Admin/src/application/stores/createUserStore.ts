import { create } from "zustand";
import type { ICreateUserStore } from "../../core/interfaces/store/createUser.store";

const initialFormValue = {
  username: "",
  firstName: "",
  lastName: "",
  email: "",
  password: "",
  verified: false,
};

export const createUserStore = create<ICreateUserStore>((set) => ({
  formValue: initialFormValue,
  formError: {},
  isSubmitting: false,

  setFormValue: (val) => set({ formValue: val }),
  setFormError: (err) => set({ formError: err }),
  setIsSubmitting: (val) => set({ isSubmitting: val }),
  reset: () =>
    set({
      formValue: initialFormValue,
      formError: {},
      isSubmitting: false,
    }),
}));
