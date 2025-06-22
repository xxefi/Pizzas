import { IFormData } from "@/app/core/interfaces/data/form.data";
import { useTranslations } from "next-intl";

export const registerValidator = (
  formData: IFormData,
  t: ReturnType<typeof useTranslations>
) => {
  const newErrors: Partial<Record<keyof IFormData, string>> = {};

  const validateField = (
    field: keyof IFormData,
    value: string | boolean | undefined
  ) => {
    switch (field) {
      case "username":
        if (!value) newErrors.username = t("UsernameRequired");
        break;
      case "firstName":
        if (!value) newErrors.firstName = t("FirstNameRequired");
        break;
      case "lastName":
        if (!value) newErrors.lastName = t("LastNameRequired");

        break;
      case "email":
        if (!value) newErrors.email = t("EmailRequired");
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value as string))
          newErrors.email = t("InvalidEmail");

        break;
      case "password":
        if (!value) newErrors.password = t("PasswordRequired");
        else if ((value as string).length < 8)
          newErrors.password = t("PasswordTooShort");

        break;
      case "confirmPassword":
        if (!value) newErrors.confirmPassword = t("ConfirmPasswordRequired");
        else if (formData.password !== value)
          newErrors.confirmPassword = t("PasswordMismatch");

        break;
      default:
        break;
    }
  };

  Object.keys(formData).forEach((field) => {
    validateField(field as keyof IFormData, formData[field as keyof IFormData]);
  });

  return newErrors;
};
