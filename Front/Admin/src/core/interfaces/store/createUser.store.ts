export interface ICreateUserStore {
  formValue: Record<string, any>;
  formError: Record<string, any>;
  isSubmitting: boolean;
  setFormValue: (val: Record<string, any>) => void;
  setFormError: (err: Record<string, any>) => void;
  setIsSubmitting: (val: boolean) => void;
  reset: () => void;
}
