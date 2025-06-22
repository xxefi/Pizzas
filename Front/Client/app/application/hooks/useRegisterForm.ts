import { IFormData } from "@/app/core/interfaces/data/form.data";
import { useState } from "react";
import { registerValidator } from "../validators/registerValidation";

export function useRegisterForm(
  formData: IFormData,
  register: () => Promise<void>,
  v: any
) {
  const [errors, setErrors] = useState<
    Partial<Record<keyof IFormData, string>>
  >({});

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const newErrors = registerValidator(formData, v);
    setErrors(newErrors);

    if (Object.keys(newErrors).length === 0) await register();
  };

  return {
    errors,
    setErrors,
    handleSubmit,
  };
}
